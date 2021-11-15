using System;
using System.Net;

namespace PmfBackend.Models.Exceptions {
    public class NotFoundTransactionException : Exception {

        public HttpStatusCode Status { get; set; }

        public NotFoundTransactionException(HttpStatusCode statusCode,string message) : base (message){
            Status = statusCode;
        }

    }
} 