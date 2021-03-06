﻿using System;
using System.Collections.Generic;

namespace Evento
{
    class Program
    {
        static void Main(string[] args)
        {
            Caja caja = new Caja();
            caja.maximoAlcanzado += Caja_maximoAlcanzado;
        }

        private static void Caja_maximoAlcanzado(object caja, CajaEventArgs evento)
        {
            throw new NotImplementedException();
            //TODO:
        }
    }

    public class Caja
    {
        int cantidadRiesgo = 200;
        
        public int Id { get; set; }
        public decimal sumatoria = 0;
        List<Ticket> tickets = new List<Ticket>();

        public void addTicket(Ticket ticket)
        {
            //Correción con la excepción
            if(ticket == null) throw new System.ArgumentException("El ticket no puede ser nulo", "ticket");
            
            sumatoria += ticket.valor;
            tickets.Add(ticket);
            
            //Simplificación de código usando funciones con 1 sola finalidad 
            if (CajaEnRiesgo()) crearEvento();
        }
        
        public void crearEvento(){
            CajaEventArgs args = new CajaEventArgs();
            args.id = Id;
            args.valor = sumatoria;
            OnMaxMoney(args);   
        }    
        
        public bool CajaEnRiesgo(){
            return sumatoria > cantidadRiesgo;
        }

        protected virtual void OnMaxMoney(CajaEventArgs evento)
        {
            CajaEventHandler handler = maximoAlcanzado;
            if(handler != null)  handler(this, evento);
        }

        public event CajaEventHandler maximoAlcanzado;
    }

    public delegate void CajaEventHandler(Object caja, CajaEventArgs evento);

    public class CajaEventArgs : EventArgs
    {
        public int id { get; set; }
        public decimal valor { get; set; }
    }

    public class Ticket
    {
        public int id { get; set; }
        public decimal valor { get; set; }
    }
}
