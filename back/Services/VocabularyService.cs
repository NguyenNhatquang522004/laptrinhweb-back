using backapi.Configuration;
using backapi.Enums;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class VocabularyService : IVocabularyRepository
    {
        private readonly ApplicationDbContext _context;
        public VocabularyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<globalResponds> GetAllAsync()
        {
            try
            {
                List<Vocabulary> list = await _context.Vocabularies.ToListAsync();

                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> GetByIdAsync(Guid id)
        {
            try
            {
                Vocabulary data = await _context.Vocabularies.FindAsync(id);

                return new globalResponds("1", "thành công", data);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> CreateAsync(Vocabulary vocabulary, Category category)
        {
            try
            {
                vocabulary.CategoryId = category.CategoryId;
                vocabulary.Category = category;
                category.Vocabularies.Add(vocabulary);
                _context.Vocabularies.Add(vocabulary);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> UpdateAsync(Guid id, Vocabulary vocabulary)
        {
            try
            {
                Vocabulary search = await _context.Vocabularies.FindAsync(id);
                _context.Vocabularies.Entry(search).CurrentValues.SetValues(vocabulary);
                _context.Vocabularies.Update(search);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> DeleteAsync(Vocabulary vocabulary)
        {
            try
            {
                _context.Vocabularies.Remove(vocabulary);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> GetByCategoryAsync(Guid categoryId)
        {
            try
            {
                List<Vocabulary> list = await _context.Vocabularies.Where(o => o.Equals(categoryId)).ToListAsync();

                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> GetByLevelAsync(enums.CefrLevel level)
        {
            try
            {
                List<Vocabulary> list = await _context.Vocabularies.Where(o => o.Level.Equals(level)).ToListAsync();

                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> GetByPartOfSpeechAsync(enums.PartOfSpeech partOfSpeech)
        {
            try
            {
                List<Vocabulary> list = await _context.Vocabularies.Where(o => o.PartOfSpeech.Equals(partOfSpeech)).ToListAsync();

                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> CreateVocabularyAndCategory(Category category, Vocabulary vocabulary)
        {
            try
            {
                vocabulary.CategoryId = category.CategoryId;
                vocabulary.Category = category;
                category.Vocabularies.Add(vocabulary);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }

        public async Task<globalResponds> DeleteVocabularyAndCategory(Category category, Vocabulary vocabulary)
        {
            try
            {
                category.Vocabularies.Remove(vocabulary);
                vocabulary.CategoryId = null;
                vocabulary.Category = null;
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công" + e.Message, null);
            }
        }
    }
}
