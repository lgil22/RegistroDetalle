﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistroDetails.Entidades
{
    public class Persona
    {

        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        //  public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual List<TelefonoDetalles> Telefonos { get; set; }

        public Persona()
        {
            PersonaId = 0;
            Nombre = string.Empty;
            // Telefono = string.Empty;
            Cedula = string.Empty;
            Direccion = string.Empty;
            FechaNacimiento = DateTime.Now;

            Telefonos = new List<TelefonoDetalles>();

        }

        public Persona(int personaid, string nombre, string cedula, string direccion, DateTime fechanacimiento)
        {
            PersonaId = personaid;
            Nombre = nombre;
            //   Telefono = telefono;
            Cedula = cedula;
            Direccion = direccion;
            FechaNacimiento = fechanacimiento;
            Telefonos = new List<TelefonoDetalles>();

        }

    }
}