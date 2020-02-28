using System;
using System.Collections.Generic;
using System.Linq;
using RestfulAPI_Produto.Models;

namespace RestfulAPI_Produto.Aplicacao
{
    public class ProdutoAplicacao
    {
        private ApiContext _contexto;

        public ProdutoAplicacao(ApiContext contexto)
        {
            _contexto = contexto;
        }

        public string Insert(TProduto produto)
        {
            try
            {
                if (produto != null)
                {
                    var xProduto = GetProdutoById(produto.Id);
                    if (xProduto == null)
                    {
                        _contexto.Add(produto);
                        _contexto.SaveChanges();
                        return "Aplicação: Produto cadastrado com sucesso.";
                    }
                    else
                    {
                        return "Aplicação: Produto já cadastrado.";
                    }
                }
                else
                {
                    return "Aplicação: Dados do Produto inválido.";
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação.";
            }
        }

        public string Update(TProduto produto)
        {
            try
            {
                if (produto != null)
                {
                    _contexto.Update(produto);
                    _contexto.SaveChanges();
                    return "Aplicação: Produto alterado com sucesso.";
                }
                else
                {
                    return "Aplicação: Dados do produto inválidos.";
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação.";
            }
        }
                
        public string UpdateCatProd(int id, int idCategoria)
        {
            try
            {
                if (id > 0 && idCategoria > 0)
                {                    
                    var xCategoria = GetCategoria(idCategoria);
                    
                    if (xCategoria == null)
                    {
                        return "Aplicação: Categoria não cadastrada.";
                    }
                    
                    var produto = _contexto.TProduto.FirstOrDefault(x => x.Id == id);

                    if (produto == null)
                    {
                        return "Aplicação: Produto não cadastrado.";
                    }

                    produto.IdCategoria = idCategoria;

                    _contexto.Update(produto);
                    _contexto.SaveChanges();

                    return "Aplicação: Categoria do produto atualizada com sucesso.";
                }                    
                else
                {
                    return "Aplicação: Dados do produto inválidos.";
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação.";
            }
        }

        public TCategoria GetCategoria(int id)
        {
            TCategoria consultaCategoria = new TCategoria();

            try
            {
                if (id <= 0)
                {
                    return null;
                }

                var xCategoria = _contexto.TCategoria.Where(x => x.Id == id).ToList();
                consultaCategoria = xCategoria.FirstOrDefault();

                if (consultaCategoria != null)
                {
                    return consultaCategoria;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return "Aplicação: Código do produto inválido.";
                }
                else
                {
                    var produto = GetProdutoById(id);
                    if (produto != null)
                    {
                        _contexto.TProduto.Remove(produto);
                        _contexto.SaveChanges();
                        return "Aplicação: Produto [" + produto.Id + " - " + produto.Nome +  "] excluído com sucesso.";
                    }
                    else
                    {
                        return "Aplicação: Produto não cadastrado.";
                    }
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação, possui dependência.";
            }
        }

        public TProduto GetProdutoByNome(string nome)
        {
            TProduto primeiroProduto = new TProduto();

            try
            {
                if (nome.Trim() == string.Empty)
                {
                    return null;
                }

                var xProduto = _contexto.TProduto.Where(x => x.Nome == nome.Trim()).ToList();
                primeiroProduto = xProduto.FirstOrDefault();

                if (primeiroProduto != null)
                {
                    return primeiroProduto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TProduto GetProdutoById(int id)
        {
            TProduto consultaProduto = new TProduto();

            try
            {
                if (id <= 0)
                {
                    return null;
                }

                var xProduto = _contexto.TProduto.Where(x => x.Id == id).ToList();
                consultaProduto = xProduto.FirstOrDefault();

                if (consultaProduto != null)
                {
                    return consultaProduto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TProduto> GetProdutoByCategoria(int idCategoria)
        {
            List<TProduto> listaDeProdutos = new List<TProduto>();

            try
            {
                if (idCategoria <= 0)
                {
                    return null;
                }
                
                listaDeProdutos = _contexto.TProduto.Where(x => x.IdCategoria == idCategoria).ToList();

                if (listaDeProdutos != null)
                {
                    return listaDeProdutos;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public List<TProduto> GetProdutoAll()
        {
            List<TProduto> listaDeProdutos = new List<TProduto>();
            try
            {                
                listaDeProdutos = _contexto.TProduto.Select(x => x).ToList();
                if (listaDeProdutos != null)
                {
                    return listaDeProdutos;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    
}
