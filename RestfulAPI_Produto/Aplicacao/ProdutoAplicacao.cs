using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public string Insert(Tproduto produto)
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
                        return "Aplicação: Código do produto já existe.";
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

        public string Update(Tproduto produto)
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
                        return "Aplicação: Cadastro da categoria não existe.";
                    }
                    
                    var produto = _contexto.Tproduto.FirstOrDefault(x => x.Id == id);

                    if (produto == null)
                    {
                        return "Aplicação: Cadastro do produto não existe.";
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

        public Tcategoria GetCategoria(int id)
        {
            Tcategoria consultaCategoria = new Tcategoria();

            try
            {
                if (id <= 0)
                {
                    return null;
                }

                var xCategoria = _contexto.Tcategoria.Where(x => x.Id == id).ToList();
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
                        _contexto.Tproduto.Remove(produto);
                        _contexto.SaveChanges();
                        return "Aplicação: Produto " + produto.Id + " - " + produto.Nome +  " excluído com sucesso.";
                    }
                    else
                    {
                        return "Aplicação: Cadastro do produto não existe.";
                    }
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação, possui dependência.";
            }
        }

        public Tproduto GetProdutoByNome(string nome)
        {
            Tproduto primeiroProduto = new Tproduto();

            try
            {
                if (nome.Trim() == string.Empty)
                {
                    return null;
                }

                var xProduto = _contexto.Tproduto.Where(x => x.Nome == nome.Trim()).ToList();
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

        public Tproduto GetProdutoById(int id)
        {
            Tproduto consultaProduto = new Tproduto();

            try
            {
                if (id <= 0)
                {
                    return null;
                }

                var xProduto = _contexto.Tproduto.Where(x => x.Id == id).ToList();
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

        public List<Tproduto> GetProdutoByCategoria(int idCategoria)
        {
            List<Tproduto> listaDeProdutos = new List<Tproduto>();

            try
            {
                if (idCategoria <= 0)
                {
                    return null;
                }
                
                listaDeProdutos = _contexto.Tproduto.Where(x => x.IdCategoria == idCategoria).ToList();

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
        
        public List<Tproduto> GetProdutoAll()
        {
            List<Tproduto> listaDeProdutos = new List<Tproduto>();
            try
            {                
                listaDeProdutos = _contexto.Tproduto.Select(x => x).ToList();
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
