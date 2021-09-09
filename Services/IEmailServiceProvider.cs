using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LODSInterviewProject.Services
{
    public interface IEmailServiceProvider
    {
        Task Execute(String toEmail, String toName);
    }
}
