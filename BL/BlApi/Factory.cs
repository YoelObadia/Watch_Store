using BlImplementation;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
   
    public static class Factory
    {
        // Singleton use
        public static IBl Get() => Bl.Instance;
    }
}
