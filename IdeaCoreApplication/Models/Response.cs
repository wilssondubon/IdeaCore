﻿using IdeaCoreInterfaces.Application.Response;
using IdeaCoreUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreApplication.Models
{
    /// <summary>
    /// respuesta basica para un solicitud basado en un tipo de modelo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : IResponse where T : class
    {
        /// <summary>
        /// modelo en el que se basa la respuesta
        /// </summary>
        public T? data { get; set; }
        /// <summary>
        /// tracker de la respuesta
        /// </summary>
        protected ITrackerResponse TrackerResponse { get; set; }
        /// <summary>
        /// status de las respuesta
        /// </summary>
        public short Status => TrackerResponse.Status;
        /// <summary>
        /// estado de exito de la solicitud
        /// </summary>
        public bool Succeeded => TrackerResponse.Succeeded;
        /// <summary>
        /// mensaje de la respuesta
        /// </summary>
        public string Message => TrackerResponse.Message;
        /// <summary>
        /// listado de errores
        /// </summary>
        public string[] Errors => TrackerResponse.Errors;
        /// <summary>
        /// inicializa una respuesta de error
        /// </summary>
        /// <param name="error">titulo del error</param>
        /// <param name="errors">listado de errores sucedidos</param>
        /// <param name="succeded">estado de exito de la solicitud</param>
        //public Response(short status, string error, string[] errors, bool succeded)
        //{
        //    TrackerResponse = new TrackerResponse(status, error, errors, succeded);
        //    Item1 = default(T);
        //}
        /// <summary>
        /// inicializa una respuesta con un modelo
        /// </summary>
        /// <param name="o">modelo de la respuesta</param>
        /// <param name="t">tracker de la respuesta</param>
        public Response(T o, ITrackerResponse t)
        {
            data = o;
            TrackerResponse = t;
        }
        /// <summary>
        /// inicializa una respuesta con un modelo que inicializa como nuevo
        /// </summary>
        /// <param name="t">tracker de la respuesta</param>
        public Response(ITrackerResponse t)
        {
            TrackerResponse = t;
            data = default;
        }
        /// <summary>
        /// inicializa una respuesta de error
        /// </summary>
        /// <param name="code">codigo de error</param>
        /// <param name="error">titulo del error</param>
        /// <param name="errors">listado de errores sucedidos</param>
        /// <param name="succeded">estado de exito de la solicitud</param>
        public Response(short code, string error, string[]? errors, bool succeded)
        {
            TrackerResponse = new TrackerResponse(code, error, errors, succeded);
            data = default;
        }
        /// <summary>
        /// inicializa una respuesta con un tracker por default codigo 200 y mensaje OK
        /// </summary>
        /// <param name="o">modelo de la respuesta</param>
        public Response(T o)
        {
            data = o;
            TrackerResponse = new TrackerResponse(200, "OK");
        }
        /// <summary>
        /// inicializa una respuesta con valores default nuevos
        /// </summary>
        public Response()
        {
            TrackerResponse = null;
            data = default;
        }
    }
}
