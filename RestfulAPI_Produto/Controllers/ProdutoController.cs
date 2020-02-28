using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI_Produto.Models;
using RestfulAPI_Produto.Aplicacao;
using Newtonsoft.Json;

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
        public IActionResult Insert([FromBody]TProduto produto)
        {
            String msg = "Inclusão de Produto por Categoria";
            try
            {
                if (!ModelState.IsValid || produto == null)
                {
                    msg = "Inclusao - Controller: Dados do produto inválidos.";
                    return BadRequest(msg);
                }
                else
                {
                    if (produto.Id != 0)
                    {
                        msg = "Inclusao - Controller: Informar zero (0) no ''Id'' para ser gerado o novo código pelo sistema.";
                        return BadRequest(msg);
                    }

                    if (produto.Nome.Trim().Equals(""))
                    {
                        msg = "Inclusao - Controller: Necessário informar o nome do produto.";
                        return BadRequest(msg);
                    }

                    produto.Nome = produto.Nome.Trim();
                    var resposta = new ProdutoAplicacao(_contexto).Insert(produto);
                    return Ok("Inclusao Produto - " + resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Inclusao - Cotroller: Não foi possível realizar a operação: [" + msg + "]");
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]TProduto produto)
        {
            String msg = "Edição de Produto por Categoria";
            try
            {
                if (!ModelState.IsValid || produto == null)
                {
                    msg = "Edicao - Controller: Dados inválidos.";
                    return BadRequest(msg);
                }
                else
                {
                    if (produto.Id <= 0)
                    {
                        msg = "Edicao - Controller: Código do produto inválido.";
                        return BadRequest(msg);
                    }

                    if (produto.Nome.Trim().Equals(""))
                    {
                        msg = "Edicao - Controller: Nome do produto não informado.";
                        return BadRequest(msg);
                    }

                    produto.Nome = produto.Nome.Trim();
                    var resposta = new ProdutoAplicacao(_contexto).Update(produto);
                    return Ok("Edicao Produto - " + resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Edicao - Controller: Não foi possível realizar a operação [" + msg + "]");
            }
        }

        [HttpPut]
        [Route("UpdateCategoriaByProduto")]        
        public IActionResult UpdateCategoriaByProduto([FromBody] string prodCat)
        {
            String msg = "Edicao Categoria de Produto";
            try
            {
                if (prodCat.Trim() == string.Empty || prodCat.Trim().Equals(""))
                {
                    return BadRequest("Edicao Cat Prod - Controller: Dados informados inválidos.");
                }
                else
                {
                    try
                    {
                        string[] texto = prodCat.Trim().Split("-");                    
                        int idProduto = Convert.ToInt32(texto[0].Trim());
                        int idCategoria = Convert.ToInt32(texto[1].Trim());
                        var resposta = new ProdutoAplicacao(_contexto).UpdateCatProd(idProduto, idCategoria);
                        return Ok("Edicao Cat Prod - " + resposta);
                    } 
                    catch (Exception)
                    {
                        return BadRequest("Edicao Cat Prod - Controller: Parâmetro com formato inválido. Exemplo correto: 2-5 , onde 2 é o Id do produto e 5 é o Id da Categoria.");
                    }                
                }
            }
            catch (Exception)
            {
                return BadRequest("Edicao Cat Prod - Cotroller: Não foi possível realizar a operação [" + msg + "]");
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
                    return BadRequest("Delete - Controller: Código do produto inválido.");
                }
                else
                {
                    var resposta = new ProdutoAplicacao(_contexto).Delete(id);
                    return Ok("Delete - Produto - " + resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Delete - Cotroller: Não foi possível realizar a operação.");
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
                        return BadRequest("Controller: Produto não cadastrado.");
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
                        return BadRequest("Controller: Produto não cadastrado.");
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