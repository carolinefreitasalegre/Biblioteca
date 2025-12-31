using Models.Models;

namespace Biblioteca.Client;

public static class DicasLivrosMock
{
    public static readonly List<DicaLivroMock> Dicas = new()
    {
        new()
        {
            Texto = "A leitura nos permite viver mil vidas antes de viver apenas uma.",
            Autor = "George R. R. Martin",
            Livro = "Inspirado em suas obras"
        },
        new()
        {
            Texto = "Algumas histórias mudam quem somos, mesmo depois de terminadas.",
            Autor = "J. K. Rowling",
            Livro = "Harry Potter"
        },
        new()
        {
            Texto = "Sonhar é o primeiro passo para transformar a realidade.",
            Autor = "Paulo Coelho",
            Livro = "O Alquimista"
        },
        new()
        {
            Texto = "Nem toda jornada precisa de um mapa, apenas de coragem.",
            Autor = "J. R. R. Tolkien",
            Livro = "O Senhor dos Anéis"
        },
        new()
        {
            Texto = "Livros são refúgios silenciosos para mentes inquietas.",
            Autor = "Clarice Lispector",
            Livro = "Obras diversas"
        },
        new()
        {
            Texto = "Cada página virada é um passo fora da rotina.",
            Autor = "Machado de Assis",
            Livro = "Obras completas"
        },
        new()
        {
            Texto = "A imaginação é o lugar onde tudo começa.",
            Autor = "Neil Gaiman",
            Livro = "American Gods"
        },
        new()
        {
            Texto = "Histórias nos ensinam aquilo que a vida demora a explicar.",
            Autor = "Antoine de Saint-Exupéry",
            Livro = "O Pequeno Príncipe"
        },
        new()
        {
            Texto = "Ler é conversar com alguém que viveu em outro tempo.",
            Autor = "Umberto Eco",
            Livro = "O Nome da Rosa"
        },
        new()
        {
            Texto = "Todo grande leitor carrega um universo dentro de si.",
            Autor = "Agatha Christie",
            Livro = "Mistérios diversos"
        }
    };
}