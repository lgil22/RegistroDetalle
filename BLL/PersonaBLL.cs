using Microsoft.EntityFrameworkCore;
using RegistroDetails.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using RegistroDetails.DAL;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;


namespace RegistroDetails.BLL
{
    public class PersonaBLL
    {
        public static bool Guardar(Persona persona)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Persona.Add(persona) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        //Este es el metodo para modificar en la base de datos
        public static bool Modificar(Persona persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                //buscar las entidades que no estan para removerlas
                var Anterior = db.Persona.Find(persona.PersonaId);
                foreach (var item in Anterior.Telefonos)

                {
                    if (!persona.Telefonos.ToList().Exists(p => p.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;
                }

                db.Entry(persona).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        //Este es el metodo para eliminar en la base de datos
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var eliminar = db.Persona.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;

                paso = (db.SaveChanges() > 0);
                //System.Data.Entity// No va
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        //Este es el metodo para buscar en la base de datos
        public static Persona Buscar(int id)
        {
            Contexto db = new Contexto();
            Persona personas = new Persona();
            try
            {
                // El Count() lo que hace es engañar al lazyloading y obligarlo a cargar los detalles 
                    personas.Telefonos.Count();
                    personas = db.Persona.Where(x => x.PersonaId == id).
                    Include(o => o.Telefonos).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return personas;
        }

        //Este es el metodo para listar o consultar lo que tenemos en la base de datos
        public static List<Persona> GetList(Expression<Func<Persona, bool>> personas)
        {
            List<Persona> Lista = new List<Persona>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.Persona.Where(personas).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}

