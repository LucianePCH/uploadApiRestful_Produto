using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI_Produto.Models;
using RestfulAPI_Produto.Aplicacao;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace RestfulAPI_Produto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private ApiContext _contexto;

        public ProdutoController(ApiContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return Ok("Index - API de Gestão de Produtos - Cadastro de Produto");
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody]Tproduto produto)
        {
            try
            {
                if (!ModelState.IsValid || produto == null)
                {
                    return BadRequest("Controller: Dados do produto inválidos.");
                }
                else
                {
                    if (produto.Id < 0)
                    {
                        return BadRequest("Controller: Informar o novo código maior que zero, ou informar zero (0) para o sistema gerar o novo código.");
                    }

                    if (produto.Nome.Trim().Equals(""))
                    {
                        return BadRequest("Controller: Necessário informar o nome do produto.");
                    }

                    produto.Nome = produto.Nome.Trim();
                    var resposta = new ProdutoAplicacao(_contexto).Insert(produto);
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
        public IActionResult Update([FromBody]Tproduto produto)
        {
            try
            {
                if (!ModelState.IsValid || produto == null)
                {
                    return BadRequest("Controller: Dados inválidos.");
                }
                else
                {
                    if (produto.Id <= 0)
                    {
                        return BadRequest("Cotroller: Código do produto inválido.");
                    }

                    if (produto.Nome.Trim().Equals(""))
                    {
                        return BadRequest("Cotroller: Nome do produto não informado.");
                    }

                    produto.Nome = produto.Nome.Trim();
                    var resposta = new ProdutoAplicacao(_contexto).Update(produto);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpPut]
        [Route("UpdateCategoriaByProduto")]        
        public IActionResult UpdateCategoriaByProduto([FromBody] string prodCat)
        {
            try
            {
                if (prodCat.Trim() == string.Empty || prodCat.Trim().Equals(""))
                {
                    return BadRequest("Controller: Dados informados inválidos.");
                }
                else
                {
                    try
                    {
                        string[] texto = prodCat.Trim().Split("-");                    
                        int idProduto = Convert.ToInt32(texto[0].Trim());
                        int idCategoria = Convert.ToInt32(texto[1].Trim());
                        var resposta = new ProdutoAplicacao(_contexto).UpdateCatProd(idProduto, idCategoria);
                        return Ok(resposta);
                    }
                    catch (Exception)
                    {
                        return BadRequest("Controller: Parâmetro com formato inválido. Exemplo correto: 2-5 , onde 2 é o Id do produto e 5 é o Id da Categoria.");
                    }                
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
                    return BadRequest("Controller: Código do produto inválido.");
                }
                else
                {
                    var resposta = new ProdutoAplicacao(_contexto).Delete(id);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpPost]
        [Route("GetProdutoByNome")]
        public IActionResult GetProdutoByNome([FromBody]string nome)
        {
            try
            {
                if (nome.Trim() == string.Empty || nome.Trim().Equals(""))
                {
                    return BadRequest("Controller: Nome do proudto inválido.");
                }
                else
                {
                    var resposta = new ProdutoAplicacao(_contexto).GetProdutoByNome(nome.Trim());
                    if (resposta != null)
                    {
                        var produtoResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(produtoResposta);
                    }
                    else
                    {
                        return BadRequest("Controller: Produto não existe.");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpPost]
        [Route("GetProdutoById")]
        public IActionResult GetProdutoById([FromBody]int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Controller: Produto inválido.");
                }
                else
                {
                    var resposta = new ProdutoAplicacao(_contexto).GetProdutoById(id);
                    if (resposta != null)
                    {
                        var produtoResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(produtoResposta);
                    }
                    else
                    {
                        return BadRequest("Controller: Produto não existe.");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpPost]
        [Route("GetProdutoByCategoria")]
        public IActionResult GetProdutoByCategoria([FromBody]int idCategoria)
        {
            try
            {
                if (idCategoria <= 0)
                {
                    return BadRequest("Controller: Categoria inválida.");
                }
                else
                {  
                    var listaDeProdutos = new ProdutoAplicacao(_contexto).GetProdutoByCategoria(idCategoria);
                    if (listaDeProdutos != null && listaDeProdutos.Count() > 0)
                    {
                        var listaResposta = JsonConvert.SerializeObject(listaDeProdutos);

                        return Ok(listaResposta);
                                                
                    }
                    else
                    {
                        return BadRequest("Controller: Categoria não possui produtos vinculados .");
                    }
                }
                                    
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }

        [HttpGet]
        [Route("GetProdutoAll")]
        public IActionResult GetProdutoAll()
        {
            try
            {
                var listaDeProdutos = new ProdutoAplicacao(_contexto).GetProdutoAll();
                if (listaDeProdutos != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeProdutos);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Controller: Nenhum produto cadastrado.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Cotroller: Não foi possível realizar a operação.");
            }
        }
    }   

}