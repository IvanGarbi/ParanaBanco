using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Notificacoes;

namespace ParanaBancoCase.Main.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private readonly INotificador _notificador;

    protected BaseController(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected bool OperacaoValida()
    {
        return !_notificador.TemNotificacao();
    }

    protected ActionResult CriarResposta(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            NotificarErroModelState(modelState);
        }

        return CriarResposta();
    }

    protected ActionResult CriarResposta(object result = null)
    {
        if (OperacaoValida())
        {
            return Ok(new
            {
                sucesso = true,
                data = result
            });
        }

        return BadRequest(new
        {
            sucesso = false,
            errors = _notificador.RetornaNotificacaoes().Select(n => n.Mensagem)
        });
    }

    protected void NotificarErroModelState(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(errorMsg);
        }
    }

    protected void NotificarErro(string mensagem)
    {
        _notificador.AdicionarNotificacao(new Notificacao(mensagem));
    }
}