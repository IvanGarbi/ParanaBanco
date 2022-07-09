using ParanaBancoCase.Business.Notificacoes;

namespace ParanaBancoCase.Business.Interfaces;

public interface INotificador
{
    bool TemNotificacao();
    List<Notificacao> RetornaNotificacaoes();
    void AdicionarNotificacao(Notificacao notificacao);
}