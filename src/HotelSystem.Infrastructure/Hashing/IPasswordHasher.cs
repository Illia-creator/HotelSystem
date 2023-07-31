using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Infrastructure.Hashing;

public interface IPasswordHasher
{
    public string Secure(string password);
    public bool Validete(string hashPassword, string inputPassword);
}
