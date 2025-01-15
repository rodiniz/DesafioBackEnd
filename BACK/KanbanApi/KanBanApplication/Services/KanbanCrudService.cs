﻿using KanBanApplication.Domain.Entities;
using KanBanApplication.Domain.Interfaces;
using KanBanApplication.Dtos;
using KanBanApplication.InfraStructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace KanBanApplication.Services;

public class KanbanCrudService:IKanbanCrudService
{
    private readonly KanbanContext _context;

    public KanbanCrudService(KanbanContext context)
    {
        _context = context;
    }

    public async Task<KanbanCardDto> Create(KanbanCardModelDto model)
    {
        var entity = KanbanMapper.DtoToEntity(model);
        entity.Id = Guid.NewGuid();
        _context.Set<KanbanCard>().Add(entity);
        await _context.SaveChangesAsync();
        return KanbanMapper.EntityToDto(entity);
    }

   

    public async Task<OneOf<NotFound,KanbanCardModelDto>> Update(Guid id, KanbanCardModelDto model)
    {
       var dbCard = await _context.Set<KanbanCard>().FindAsync(id);
       if (dbCard == null)
       {
           return new NotFound();
       }
       dbCard.Conteudo = model.Conteudo;
       dbCard.Lista = model.Lista;
       dbCard.Titulo = model.Titulo;
       _context.Set<KanbanCard>().Update(dbCard);
       await _context.SaveChangesAsync();
       return  KanbanMapper.EntityToDto(dbCard);
    }

    public async Task<OneOf<NotFound,List<KanbanCardDto>>> Delete(Guid idEntity)
    {
        var entity = await _context.Set<KanbanCard>().FindAsync(idEntity);
        if (entity == null)
        {
            return new NotFound();
        }
        _context.Set<KanbanCard>().Remove(entity);
        await _context.SaveChangesAsync();
        return await GetAll();
    }

    public async Task<List<KanbanCardDto>> GetAll()
    {
        return await _context.Set<KanbanCard>().EntityToDto().ToListAsync();
    }

    public async Task<KanbanCardDto?> Get(Guid id)
    {
        var entity = await _context.Set<KanbanCard>().FindAsync(id);
        return entity == null ? null : KanbanMapper.EntityToDto(entity);
    }
}