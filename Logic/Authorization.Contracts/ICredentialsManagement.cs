using CrossCutting.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAuthorization.Contracts
{
    public interface ICredentialsManagement
    {
        Credentials Load();
    }
}
