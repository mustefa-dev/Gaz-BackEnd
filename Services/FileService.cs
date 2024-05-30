using AutoMapper;
using BackEndStructuer.DATA.DTOs.Files;
using BackEndStructuer.Repository;
using File = Gaz_BackEnd.Entities.File;

namespace Gaz_BackEnd.Services;

public interface IFileService{
    Task<(List<FileDto>? file, string? error)> add(FileForm fileForm);
    Task<(List<FileDto> file, int? totalCount, string? error)> GetAll(int _pageNumber = 0);
    Task<(FileDto? file, string? error)> GetById(Guid id);
    Task<(FileDto? file, string? error)> update(FileForm fileForm, Guid id);
    Task<(FileDto? file, string?)> Delete(Guid id);
}

public class FileService : IFileService{

    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IPhotoService _photoService;


        public FileService(IMapper mapper, IRepositoryWrapper repositoryWrapper, IPhotoService photoService) {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        _photoService = photoService;
    }

    public async Task<(List<FileDto>? file, string? error)> add(FileForm fileForm) {
        //var file = _mapper.Map<File>(fileForm);
          List<FileDto> fileDtoList = new List<FileDto>();
        if (fileForm.File != null) {
            foreach (var item in fileForm.File)
            {
                  
            var Path = await _photoService.UploadImages(item);
                var file = new File
                {
                    Path = Path,
                };
            var result = await _repositoryWrapper.File.Add(file);
             if(result == null)
                {
                    return (null, "file could not add");
                }
                fileDtoList.Add( _mapper.Map<FileDto>(result));
            }
            return (fileDtoList, null);
        }
        else
        {
            return  (null, "The file cannot be null ") ;
        }
        
    }

    public async Task<(List<FileDto> file, int? totalCount, string? error)> GetAll(int _pageNumber = 0) {
        var (files, totalCount) = await _repositoryWrapper.File.GetAll(x => x.Deleted != true, _pageNumber);
        var result = _mapper.Map<List<FileDto>>(files);
        return (result, totalCount, null);
    }

    public async Task<(FileDto? file, string? error)> GetById(Guid id) {
        var file = await _repositoryWrapper.File.GetById(id);
        var fileDto = _mapper.Map<FileDto>(file);
        return (fileDto, null);
    }

    public async Task<(FileDto? file, string? error)> update(FileForm fileForm, Guid id) {
        var file = await _repositoryWrapper.File.GetById(id);
        if (file == null) {
            return (null, "file not found");
        }

        file = _mapper.Map(fileForm, file);
        var response = await _repositoryWrapper.File.Update(file);
        var fileDto = _mapper.Map<FileDto>(response);
        return response == null ? (null, "file could not be updated") : (fileDto, null);
    }

    public async Task<(FileDto? file, string?)> Delete(Guid id) {
        var file = await _repositoryWrapper.File.GetById(id);
        if (file == null) {
            return (null, "file not found");
        }

        file.Deleted = true;
        var response = await _repositoryWrapper.File.Update(file);
        var fileDto = _mapper.Map<FileDto>(response);
        return response == null ? (null, "file could not be deleted") : (fileDto, null);
    }
}