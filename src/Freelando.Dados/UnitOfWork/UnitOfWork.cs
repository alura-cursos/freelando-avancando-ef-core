using Freelando.Dados.Repository;
using Freelando.Dados.Repository.Interfaces;

namespace Freelando.Dados.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private EspecialidadeRepository especialidadeRepository;
    private ClienteRepository clienteRepository;
    private ContratoRepository contratoRepository;
    private ProfissionalRepository profissionalRepository;
    private ServicoRepository servicoRepository;
    private CandidaturaRepository candidaturaRepository;
    private ProjetoRepository projetoRepository;
    public ICandidaturaRepository CandidaturaRepository
    {
        get
        {
            if (candidaturaRepository==null)
            {
                candidaturaRepository = new CandidaturaRepository(context!);
            }

            return candidaturaRepository;
        }
    }
    public IServicoRepository ServicoRepository
    {
        get
        {
            if (servicoRepository == null)
            {
                servicoRepository = new ServicoRepository(context!);
            }
            return servicoRepository;
        }
    }
    public IEspecialidadeRepository EspecialidadeRepository
    {

        get
        {
            if (especialidadeRepository == null)
            {
                especialidadeRepository = new EspecialidadeRepository(context!);
            }
            return especialidadeRepository;
        }
    }
    public IClienteRepository ClienteRepository { 
        get {
            if (clienteRepository == null)
            {
                clienteRepository = new ClienteRepository(context!);
            }
            return clienteRepository;
        }
    }
    public IContratoRepository ContratoRepository
    {
        get
        {
            if (contratoRepository == null)
            {
                contratoRepository = new ContratoRepository(context!);
            }
            return contratoRepository;
        }
    }
    public IProfissionalRepository ProfissionalRepository
    {
        get
        {
            if (profissionalRepository == null)
            {
                profissionalRepository = new ProfissionalRepository(context!);
            }
            return profissionalRepository;
        }
    }
    public IProjetoRepository ProjetoRepository
    {
        get
        {
            if (projetoRepository == null)
            {
                projetoRepository = new ProjetoRepository(context!);
            }
            return projetoRepository;
        }
    }

    public FreelandoContext contexto => context!;

    public FreelandoContext? context;

    public UnitOfWork(FreelandoContext? context)
    {
        this.context = context;
    }

    public async Task Commit()
    {
        await context!.SaveChangesAsync();
    }
    public void Dispose()
    {
        context!.Dispose();
    }
}

