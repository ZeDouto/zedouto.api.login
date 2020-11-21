namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface IJwtService<TModel, TToken> where TModel : class where TToken : class
    {
        TToken GetToken(TModel model);
    }
}