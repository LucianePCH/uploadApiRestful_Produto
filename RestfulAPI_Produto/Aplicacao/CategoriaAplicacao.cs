using System;
using System.Collections.Generic;
using System.Linq;
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

        public string Insert(TCategoria categoria)
        {
            TCategoria TCategoriaInsert = new TCategoria();

            String msg = "Aplicacao: Insert(categoria)";

            try
            {
                if (categoria == null)
                {
                    msg = "Aplicacao: Categoria nula.";
                    return msg;
                }

                var xCategoria = GetCategoriaByNome(categoria.Nome);
                if (xCategoria != null)               
                {
                    msg = "Aplicacao: Nome já cadastrado.";
                    return msg;
                }

                msg = "Add(categoria)";
                _contexto.Add(categoria);

                msg = "SaveChanges()";
                _contexto.SaveChanges();

                return "Aplicacao: Categoria cadastrada com sucesso!";            
               
            }
            catch (Exception)
            {               
                return "Aplicacao - Não foi possível realizar a operação: [" + msg + "]";                
            }
        }

        public string Update(TCategoria categoria)
        {
            String msg = "Aplicacao: Update";
            try
            {
                if (categoria != null)
                {
                    msg = "Update(categoria)";
                    _contexto.Update(categoria);

                    msg = "SaveChanges()";
                    _contexto.SaveChanges();

                    return "Aplicacao: Categoria alterada com sucesso!";                 
                }
                else
                {
                    msg = "Aplicacao: Categoria não cadastrada.";
                    return msg;
                }
            }
            catch (Exception)
            {                
                return "Aplicacao: Não foi possível realizar a operação: [" + msg + "]" ;
            }
        }

        public string Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return "Aplicacao: Código da categoria inválido.";
                }
                else
                {                    
                    var categoria = GetCategoriaById(id);
                    if (categoria != null)
                    {
                        _contexto.TCategoria.Remove(categoria);
                        _contexto.SaveChanges();
                        return "Aplicacao: Categoria [" + categoria.Id + " - " + categoria.Nome + "] excluída com sucesso!";
                    }
                    else
                    {
                        return "Aplicacao: Código da categoria não cadastrado.";
                    }
                }
            }
            catch (Exception)
            {
                return "Aplicacao: Não foi possível realizar a operação, possui dependência.";
            }
        }

        public TCategoria GetCategoriaByNome(string nome)
        {
            TCategoria primeiraCategoria = new TCategoria();

            try
            {
                if (nome == string.Empty)
                {
                    return null;
                }

                var xCategoria = _contexto.TCategoria.Where(x => x.Nome == nome).ToList();
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

        public TCategoria GetCategoriaById(int id)
        {
            TCategoria consultaCategoria = new TCategoria();

            try
            {
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

        public List<TCategoria> GetCategoriaAll()
        {
            List<TCategoria> listaDeCategorias = new List<TCategoria>();

            try
            {                
                listaDeCategorias = _contexto.TCategoria.Select(x => x).ToList();
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
