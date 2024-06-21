using Freelando.Dados.Repository;

namespace Freelando.Dados.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private EspecialidadeRepository especialidadeRepository;
    public IEspecialidadeRepository EspecialidadeRepository
    {

        get
        {
            if (especialidadeRepository == null)
            {
                especialidadeRepository = new EspecialidadeRepository(context);
            }
            return especialidadeRepository;
        }
    }

    public FreelandoContext? context;

    public UnitOfWork(FreelandoContext? context)
    {
        this.context = context;
    }


    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }
    public void Dispose()
    {
        context.Dispose();
    }
}

