using Appointment.Service.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Service
{
    public class RebbitMqConnection : IRebbitMqConnection
    {
        private IConnection? _connection;
        public IConnection connection => _connection!;
        public RebbitMqConnection ()
        {
            InitializeConnection();
        }
        private void InitializeConnection()
        {
            var fectory = new ConnectionFactory
            {
                HostName= "localhost",
            };
            _connection=fectory.CreateConnection();
        }
    }
}
