using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Insert([FromBody]Tcategoria categoria)
        {
            try
            {
                if (!ModelState.IsValid || categoria == null)
                {
                    return BadRequest("Cotroller: Dados da categoria inválidos.");
                }
                else
                {
                    if (categoria.Id < 0)
                    {
                        return BadRequest("Controller: Informar o novo código maior que zero, ou informar zero (0) para o sistema gerar o novo código.");
                    }

                    if (categoria.Nome.Trim().Equals(""))
                    {
                        return BadRequest("Controller: Necessário informar o nome da categoria.");
                    }

                    categoria.Nome = categoria.Nome.Trim();
                    var resposta = new CategoriaAplicacao(_contexto).Insert(categoria);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]Tcategoria categoria)
        {
            try
            {
                if (!ModelState.IsValid || categoria == null)
                {
                    return BadRequest("Cotroller: Dados da categoria inválidos.");
                }
                else
                {
                    if (categoria.Id <= 0)
                    {
                        return BadRequest("Cotroller: Código da categoria inválido.");
                    }

                    if (categoria.Nome.Trim().Equals(""))
                    { 
                        return BadRequest("Cotroller: Nome da categoria não informado.");
                    }

                    var xCategoria = new CategoriaAplicacao(_contexto).GetCategoriaByNome(categoria.Nome.Trim());
                    if (xCategoria != null)
                    {
                        return BadRequest("Cotroller: Nome da categoria já existe.");
                    }

                    categoria.Nome = categoria.Nome.Trim();
                    var resposta = new CategoriaAplicacao(_contexto).Update(categoria);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
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
                    return BadRequest("Cotroller: Código da categoria inválido.");
                }
                else
                {
                    var resposta = new CategoriaAplicacao(_contexto).Delete(id);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
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
                        return BadRequest("Cotroller: Categoria não existe.");
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
                        return BadRequest("Cotroller: Categoria não existe.");
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