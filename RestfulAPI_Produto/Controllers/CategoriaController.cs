using System;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI_Produto.Models;
using RestfulAPI_Produto.Aplicacao;
using Newtonsoft.Json;

namespace RestfulAPI_Produto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private ApiContext _contexto;

        public CategoriaController(ApiContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return Ok("Index - API de Gestão de Produtos - Cadastro de Categoria");
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody]TCategoria categoria)
        {
            String msg = "Inclusão de Categoria";
            try
            {
                if (!ModelState.IsValid || categoria == null)
                {
                    msg = "Cotroller: Dados da categoria inválidos.";
                    return BadRequest(msg);
                }
                else
                {
                    if (categoria.Id != 0)
                    {
                        return BadRequest("Controller: Informar zero (0) no ''Id'' para ser gerado o novo código pelo sistema.");
                    }
                    if (categoria.Nome.Trim().Equals(""))
                    {
                        msg = "Controller: Necessário informar o nome da categoria.";
                        return BadRequest(msg);                        
                    }

                    categoria.Nome = categoria.Nome.Trim();
                    var resposta = new CategoriaAplicacao(_contexto).Insert(categoria);
                    return Ok("Inclusao Categoria - " + resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Controller - Não foi possível realizar a operação: [" + msg + "]");                
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]TCategoria categoria)
        {
            String msg = "Edicao de Categoria";
            try
            {
                if (!ModelState.IsValid || categoria == null)
                {
                    return BadRequest("Edicao - Cotroller: Dados da categoria inválidos.");
                }
                else
                {
                    if (categoria.Id <= 0)
                    {
                        return BadRequest("Edicao - Cotroller: Código da categoria inválido.");
                    }
                                        
                    if (categoria.Nome.Trim().Equals(""))
                    { 
                        return BadRequest("Edicao - Cotroller: Nome da categoria não informado.");
                    }

                    var xCategoria = new CategoriaAplicacao(_contexto).GetCategoriaByNome(categoria.Nome.Trim());
                    if (xCategoria != null)
                    {
                        return BadRequest("Edicao - Cotroller: Nome da categoria já cadastrado.");
                    }

                    categoria.Nome = categoria.Nome.Trim();
                    var resposta = new CategoriaAplicacao(_contexto).Update(categoria);
                    return Ok("Edicao Categoria: " + resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Edicao - Cotroller: Não foi possível realizar a operação [" + msg + "]");
            } 
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody]int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Delete - Cotroller: Código da categoria inválido.");
                }
                else
                {
                    var resposta = new CategoriaAplicacao(_contexto).Delete(id);
                    return Ok("Delete - Categoria - " + resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Delete - Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpPost]
        [Route("GetCategoriaByNome")]
        public IActionResult GetCategoriaByNome([FromBody]string nome)
        {
            try
            {
                if (nome.Trim() == string.Empty)
                {
                    return BadRequest("Cotroller: Nome da categoria inválido.");
                }
                else
                {
                    var resposta = new CategoriaAplicacao(_contexto).GetCategoriaByNome(nome.Trim());
                    if (resposta != null)
                    {
                        var categoriaResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(categoriaResposta);
                    }
                    else
                    {
                        return BadRequest("Cotroller: Categoria não cadastrada.");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpPost]
        [Route("GetCategoriaById")]
        public IActionResult GetCategoriaById([FromBody]int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Cotroller: Código da categoria inválido.");
                }
                else
                {
                    var resposta = new CategoriaAplicacao(_contexto).GetCategoriaById(id);
                    if (resposta != null)
                    {
                        var categoriaResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(categoriaResposta);
                    }
                    else
                    {
                        return BadRequest("Cotroller: Categoria não cadastrada.");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpGet]
        [Route("GetCategoriaAll")]
        public IActionResult GetCategriaAll()
        {
            try
            {
                var listaDeCategorias = new CategoriaAplicacao(_contexto).GetCategoriaAll();
                if (listaDeCategorias != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeCategorias);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Cotroller: Nenhuma categoria cadastrada.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }
    }
}