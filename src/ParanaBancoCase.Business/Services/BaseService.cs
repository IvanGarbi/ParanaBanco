﻿using FluentValidation.Results;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Notificacoes;

namespace ParanaBancoCase.Business.Services;

public abstract class BaseService
{
    private readonly INotificador _notificador;

    protected BaseService(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificar(error.ErrorMessage);
        }
    }

    protected void Notificar(string mensagem)
    {
        _notificador.AdicionarNotificacao(new Notificacao(mensagem));
    }
}