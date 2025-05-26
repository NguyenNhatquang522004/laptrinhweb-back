using backapi.Configuration;
using backapi.Enums;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class GrammarRuleService : IGrammarRuleRepository
    {
        private readonly ApplicationDbContext _context;
        public GrammarRuleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<globalResponds> GetAllAsync()
        {
            try
            {
                List<GrammarRule> grammarRules = await _context.GrammarRules.ToListAsync();
                return new globalResponds("1", "thành công", grammarRules);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }
        public async Task<globalResponds> CreateAsync(GrammarRule grammarRule, Category category)
        {
            try
            {
                grammarRule.Category = category;
                grammarRule.CategoryId = category.CategoryId;
                category.GrammarRules.Add(grammarRule);
                _context.GrammarRules.Add(grammarRule);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetByIdAsync(Guid id)
        {
            try
            {
                GrammarRule grammarRules = await _context.GrammarRules.FindAsync(id);
                return new globalResponds("1", "thành công", grammarRules);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }

        public async Task<globalResponds> UpdateAsync(Guid id, GrammarRule grammarRule)
        {
            try
            {
                GrammarRule search = await _context.GrammarRules.FindAsync(id);
                if (search == null)
                {
                    return new globalResponds("0", "không tìm thấy quy tắc ngữ pháp với ID: " + id, null);
                }
                _context.Entry(search).CurrentValues.SetValues(grammarRule);
                _context.GrammarRules.Update(search);
                _context.SaveChanges();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }

        public async Task<globalResponds> DeleteAsync(Guid id)
        {
            try
            {
                GrammarRule grammarRule = await _context.GrammarRules.FindAsync(id);
                if (grammarRule == null)
                {
                    return new globalResponds("0", "không tìm thấy quy tắc ngữ pháp với ID: " + id, null);
                }
                _context.GrammarRules.Remove(grammarRule);
                await _context.SaveChangesAsync();

                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetByCategoryAsync(Guid categoryId)
        {
            try
            {
                List<GrammarRule> grammarRules = await _context.GrammarRules.Where(o => o.CategoryId == categoryId).ToListAsync();
                return new globalResponds("1", "thành công", grammarRules);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetByLevelAsync(enums.CefrLevel level)
        {
            try
            {
                List<GrammarRule> grammarRules = await _context.GrammarRules.Where(o => o.Level == level).ToListAsync();
                return new globalResponds("1", "thành công", grammarRules);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }

        public async Task<globalResponds> CreateGrammaRuleAndCategory(Category category, GrammarRule grammarRule)
        {
            try
            {
                grammarRule.Category = category;
                grammarRule.CategoryId = category.CategoryId;
                category.GrammarRules.Add(grammarRule);
                _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }

        public async Task<globalResponds> DeleteGrammaRuleAndCategory(Category category, GrammarRule grammarRule)
        {
            try
            {
                List<GrammarRule> grammarRules = await _context.GrammarRules.ToListAsync();
                return new globalResponds("1", "thành công", grammarRules);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công: " + e.Message, null);
            }
        }
    }
}
