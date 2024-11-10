using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LeaveManagementSystem.Web.Services;

public class LeaveTypesService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypesService
{
    public async Task<List<LeaveTypeReadOnlyVM>> GetAllLeaveTypes()
    {
        // var data = SELECT * FROM LeaveTypes
        var data = await _context.LeaveTypes.ToListAsync();
        // convert the datamodel into a view model - UseAutoMapper
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
        return viewData;
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var data = _context.LeaveTypes.FirstOrDefault(x => x.Id == id);
        if (data == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Remove(int id)
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (data != null)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Edit(LeaveTypeEditVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Update(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task Create(LeaveTypeCreateVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Add(leaveType);
        await _context.SaveChangesAsync();
    }

    public bool LeaveTypeExists(int id)
    {
        return (_context.LeaveTypes?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.LeaveTypeName.ToLower().Equals(name));
    }
    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        var lowercaseName = leaveTypeEdit.LeaveTypeName.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.LeaveTypeName.ToLower().Equals(lowercaseName) && q.Id != leaveTypeEdit.Id);
    }
}

// The above method has all of the logic that the method in the controller had originally,
// we are following the principle that no database calls are being made from the controller,
// but from the service layer.