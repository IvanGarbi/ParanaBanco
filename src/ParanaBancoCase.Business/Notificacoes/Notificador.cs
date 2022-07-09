using ParanaBancoCase.Business.Interfaces;

namespace ParanaBancoCase.Business.Notificacoes;

public class Notificador : INotificador
{
    private List<Notificacao> _notificacoes;

    public Notificador()
    {
        _notificacoes = new List<Notificacao>(0);
    }

    public bool TemNotificacao()
    {
        return _notificacoes.Any();
    }

    public List<Notificacao> RetornaNotificacaoes()
    {
        return _notificacoes;
    }

    public void AdicionarNotificacao(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }
}