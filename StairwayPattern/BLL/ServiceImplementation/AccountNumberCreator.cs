using BLL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    public class AccountNumberCreator : IAccountNumberCreateService
    {
        public string Create(int number) => (++number).ToString();
    }
}
