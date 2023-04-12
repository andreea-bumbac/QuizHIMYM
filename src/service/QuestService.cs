using accesa.src.domain;
using accesa.src.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.service;

public class QuestService : IQuestService
{
    private IQuestRepo questRepository;

    public QuestService(IQuestRepo questRepository)
    {
        this.questRepository = questRepository;

    }



    public Quest FindById(long id)
    {
        return questRepository.FindById(id);
    }

    public IEnumerable<Quest> FindAll()
    {
        return questRepository.FindAll();
    }

    public void Save(Quest newEntity)
    {

        questRepository.Save(newEntity);
    }

    public void Delete(long id)
    {
        questRepository.Delete(id);
    }

}