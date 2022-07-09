using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Models;
using ParanaBancoCase.Main.ViewModels;

namespace ParanaBancoCase.Main.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : BaseController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteRepository clienteRepository, IMapper mapper, IClienteService clienteService)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _clienteService = clienteService;
        }


        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.BuscarTodos());
        }

        // GET api/<ClienteController>/5
        [HttpGet("{email}")]
        public async Task<ActionResult<ClienteViewModel>> ObterPorEemail(string email)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.BuscarPorEmail(email));
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteViewModel));

            return clienteViewModel;
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{email}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(string email, ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return clienteViewModel;
            }


            await _clienteService.Atualizar(_mapper.Map<Cliente>(clienteViewModel));

            return clienteViewModel;
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{email}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(string email)
        {
            var clienteViewModel = _mapper.Map<ClienteViewModel>(_clienteRepository.BuscarPorEmail(email).Result);

            if (clienteViewModel == null)
            {
                return NotFound();
            }

            await _clienteService.Remover(email);

            return Ok();
        }
    }
}
