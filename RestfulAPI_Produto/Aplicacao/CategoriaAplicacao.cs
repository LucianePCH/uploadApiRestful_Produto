using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulAPI_Produto.Models;

namespace RestfulAPI_Produto.Aplicacao
{
    public class CategoriaAplicacao
    {
        private ApiContext _contexto;

        public CategoriaAplicacao(ApiContext contexto)
        {
            _contexto = contexto;
        }

        public string Insert(Tcategoria categoria)
        {
            try
            {
                if (categoria != null)
                {
                    var xCategoria = GetCategoriaByNome(categoria.Nome.Trim());
                    if (xCategoria == null)
                    {
                        _contexto.Add(categoria);
                        _contexto.SaveChanges();
                        return "Aplicação: Categoria cadastrada com sucesso!";
                    }
                    else
                    {
                        return "Aplicação: Nome da categoria já existe.";
                    }

                }
                else
                {
                    return "Aplicação: Dados da categoria inválidos.";
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação.";
            }
        }

        public string Update(Tcategoria categoria)
        {
            try
            {
                if (categoria != null)
                {                    
                    _contexto.Update(categoria);
                    _contexto.SaveChanges();
                    return "Aplicação: Categoria alterada com sucesso!";                 
                }
                else
                {
                    return "Aplicação: Categoria não existe.";
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação.";
            }
        }

        public string Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return "Aplicação: Código da categoria inválido.";
                }
                else
                {                    
                    var categoria = GetCategoriaById(id);
                    if (categoria != null)
                    {
                        _contexto.Tcategoria.Remove(categoria);
                        _contexto.SaveChanges();
                        return "Aplicação: Categoria " + categoria.Id + " - " + categoria.Nome + " excluída com sucesso!";
                    }
                    else
                    {
                        return "Aplicação: Código da categoria não existe.";
                    }
                }
            }
            catch (Exception)
            {
                return "Aplicação: Não foi possível realizar a operação, possui dependência.";
            }
        }

        public Tcategoria GetCategoriaByNome(string nome)
        {
            Tcategoria primeiraCategoria = new Tcategoria();

            try
            {
                if (nome == string.Empty)
                {
                    return null;
                }

                var xCategoria = _contexto.Tcategoria.Where(x => x.Nome == nome).ToList();
                primeiraCategoria = xCategoria.FirstOrDefault();

                if (primeiraCategoria != null)
                {
                    return primeiraCategoria;
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

        public Tcategoria GetCategoriaById(int id)
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

        public List<Tcategoria> GetCategoriaAll()
        {
            List<Tcategoria> listaDeCategorias = new List<Tcategoria>();

            try
            {                
                listaDeCategorias = _contexto.Tcategoria.Select(x => x).ToList();
                if (listaDeCategorias != null)
                {
                    return listaDeCategorias;
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
