using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Application.HashingUnits;

public interface IPasswordHasher
{
    public string Secure(string password);
    public bool Validete(string hashPassword, string inputPassword);
}
