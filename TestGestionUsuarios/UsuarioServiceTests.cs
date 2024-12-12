using AutoMapper;
using Domain.Common.Interfaces;
using Domain.DTO;
using Domain.Models;
using Infrastucture.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestFixture]
public class UsuarioServiceTests
{
    private Mock<IUsuarioRepository> _mockUsuarioRepository;
    private Mock<IMapper> _mockMapper;
    private UsuarioService _usuarioService;

    [SetUp]
    public void SetUp()
    {
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _mockMapper = new Mock<IMapper>();
        _usuarioService = new UsuarioService(_mockUsuarioRepository.Object, _mockMapper.Object);
    }

    #region Create
    [Test]
    public async Task Create_ShouldReturnUsuarioDto_WhenUsuarioIsCreated()
    {
        // Arrange
        var dtoUsuario = new UsuarioDTO { Nombre = "Juan", Mail= "mati@gmail.com" };
        var usuarioEntidad = new Usuario { Nombre = "Juan", Mail = "mati@gmail.com", Id = 0 };
        var usuarioCreado = new Usuario { Id = 1, Nombre = "Juan", Mail = "mati@gmail.com" };
        var usuarioDtoCreado = new UsuarioDTO { Id = 1, Nombre = "Juan", Mail = "mati@gmail.com" };

        _mockMapper.Setup(m => m.Map<Usuario>(dtoUsuario)).Returns(usuarioEntidad);
        _mockUsuarioRepository.Setup(r => r.CreateUsuario(usuarioEntidad)).ReturnsAsync(usuarioCreado);
        _mockMapper.Setup(m => m.Map<UsuarioDTO>(usuarioCreado)).Returns(usuarioDtoCreado);

        // Act
        var result = await _usuarioService.Create(dtoUsuario);

        // Assert
        Assert.AreEqual(usuarioDtoCreado, result);
    }
    #endregion

    #region Delete
    [Test]
    public async Task DeleteUsuario_ShouldReturnId_WhenUsuarioIsDeleted()
    {
        // Arrange
        var usuarioId = 1;
        _mockUsuarioRepository.Setup(r => r.DeleteUsuario(usuarioId)).ReturnsAsync(usuarioId);

        // Act
        var result = await _usuarioService.DeleteUsario(usuarioId);

        // Assert
        Assert.AreEqual(usuarioId, result);
    }
    #endregion

    #region GetUsuarios
    [Test]
    public async Task GetUsuarios_ShouldReturnUsuarioDtoList_WhenUsuariosExist()
    {
        // Arrange
        var nombre = "Juan";
        var usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Juan", Mail= "mati@gmail.com" },
            new Usuario { Id = 2, Nombre = "Juan",  Mail= "mati@gmail.com" }
        };
        var usuariosDto = new List<UsuarioDTO>
        {
            new UsuarioDTO { Id = 1, Nombre = "Juan", Mail= "mati@gmail.com" },
            new UsuarioDTO { Id = 2, Nombre = "Juan",  Mail= "mati@gmail.com" }
        };

        _mockUsuarioRepository.Setup(r => r.GetUsuarioByValue(nombre)).ReturnsAsync(usuarios);
        _mockMapper.Setup(m => m.Map<List<UsuarioDTO>>(usuarios)).Returns(usuariosDto);

        // Act
        var result = await _usuarioService.GetUsuarios(nombre);

        // Assert
        Assert.AreEqual(usuariosDto, result);
    }
    #endregion

    #region Update
    [Test]
    public async Task UpdateUsuario_ShouldReturnId_WhenUsuarioIsUpdated()
    {
        // Arrange
        var dtoUsuario = new UsuarioDTO { Id = 1, Nombre = "Juan", Mail = "mati@gmail.com" };
        var usuarioEntidad = new Usuario { Id = 1, Nombre = "Juan", Mail = "mati@gmail.com" };
        var usuarioExistente = new Usuario { Id = 1, Nombre = "Juan", Mail = "mati@gmail.com" };

        _mockMapper.Setup(m => m.Map<Usuario>(dtoUsuario)).Returns(usuarioEntidad);
        _mockUsuarioRepository.Setup(r => r.GetUsuarioById(dtoUsuario.Id)).ReturnsAsync(usuarioExistente);
        _mockMapper.Setup(m => m.Map(dtoUsuario, usuarioExistente));
        _mockUsuarioRepository.Setup(r => r.UpdateUsuario(usuarioExistente)).ReturnsAsync(dtoUsuario.Id);

        // Act
        var result = await _usuarioService.UpdateUsario(dtoUsuario);

        // Assert
        Assert.AreEqual(dtoUsuario.Id, result);
    }

    [Test]
    public void UpdateUsuario_ShouldThrowKeyNotFoundException_WhenUsuarioDoesNotExist()
    {
        // Arrange
        var dtoUsuario = new UsuarioDTO { Id = 1, Nombre = "Juan", Mail = "mati@gmail.com" };
        _mockUsuarioRepository.Setup(r => r.GetUsuarioById(dtoUsuario.Id)).ReturnsAsync((Usuario)null);

        // Act & Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await _usuarioService.UpdateUsario(dtoUsuario));
    }
    #endregion
}
