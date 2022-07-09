using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Models;
using ParanaBancoCase.Main.ViewModels;

namespace ParanaBancoCase.Main.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : BaseController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteRepository clienteRepository, 
                                 IMapper mapper, 
                                 IClienteService clienteService,
                                 INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _clienteService = clienteService;
        }


        // GET
        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> BuscarTodos()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.BuscarTodos());
        }

        // GET email
        [HttpGet("{email}")]
        public async Task<ActionResult<ClienteViewModel>> BuscarPorEmail(string email)
        {
            var cliente = await _clienteRepository.BuscarPorEmail(email);


            if (cliente == null)
            {
                return NotFound();
            }

            return _mapper.Map<ClienteViewModel>(cliente);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CriarResposta(ModelState);
            }

            await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteViewModel));

            return CriarResposta(clienteViewModel);
        }

        // PUT
        [HttpPut("{email}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(string email, ClienteViewModel clienteViewModel)
        {
            var cliente = await _clienteRepository.BuscarPorEmail(email);

            if (cliente == null)
            {
                NotificarErro("Não existe nenhum cliente com este e-mail.");
                return CriarResposta(clienteViewModel);
            }

            if (!ModelState.IsValid)
            {
                return CriarResposta(ModelState);
            }

            var clienteAtualizado = _mapper.Map<Cliente>(clienteViewModel);

            clienteAtualizado.Id = cliente.Id;

            await _clienteService.Atualizar(clienteAtualizado);

            return CriarResposta(clienteViewModel);
        }

        // DELETE
        [HttpDelete("{email}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(string email)
        {
            var cliente = await _clienteRepository.BuscarPorEmail(email);

            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteService.Remover(email);

            return CriarResposta(_mapper.Map<ClienteViewModel>(cliente));
        }
    }
}
