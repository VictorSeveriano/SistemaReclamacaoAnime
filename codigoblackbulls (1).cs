// Sistema de reclamações de animes
// Grupo Black Bulls: Gustavo Fortunato e Victor Severiano

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Catalogo catalogo = new Catalogo();
        catalogo.CarregarReclamacoes("reclamacoes.txt");

        // Cadastro usuario print
        Console.WriteLine("\n--- BEM VINDO À CENTRAL DE RECLAMAÇÕES BLACK BULLS ---");
        Console.WriteLine("Digite o seu nome e sobrenome:");
        string nome = Console.ReadLine();
        int idade   = 0;
        bool idadeValida = false;

        while (!idadeValida)
        {
            Console.WriteLine("Digite a sua idade:");
            idadeValida = int.TryParse(Console.ReadLine(), out idade);
            if (!idadeValida)
            {
                Console.WriteLine("Por favor, insira uma idade válida.");
            }
        }

        Usuario usuario = new Usuario(nome, idade);

        // Loop menu principal
        while (true)
        {
            Console.WriteLine("\n--- CENTRAL DE RECLAMAÇÕES BLACK BULLS ---");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Informar um erro em algum anime");
            Console.WriteLine("2 - Pesquisar animes pelo nome");
            Console.WriteLine("3 - Pesquisar animes pelo gênero");
            Console.WriteLine("4 - Ver reclamações de um anime");
            Console.WriteLine("5 - Ver todas as reclamações registradas");
            Console.WriteLine("6 - Encerrar atendimento");

            int opcao        = 0;
            bool opcaoValida = false;

            while (!opcaoValida)
            {
                opcaoValida     = int.TryParse(Console.ReadLine(), out opcao);
                if (!opcaoValida || opcao < 1 || opcao > 6)
                {
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção entre 1 e 6.");
                    opcaoValida = false;
                }
            }

            if      (opcao == 1)
                catalogo.MenuReclamacaoAnime(usuario);  
            else if (opcao == 2)
                catalogo.PesquisarPorAnime(usuario); 
            else if (opcao == 3)
                catalogo.PesquisarPorGenero(); 
            else if (opcao == 4)
                catalogo.ExibirReclamacoesDeAnimeEspecifico();
            else if (opcao == 5)
            {
                catalogo.ExibirRelatorioDeReclamacoes();
            }
            else if (opcao == 6)
            {
                catalogo.GravarReclamacoes("reclamacoes.txt");
                Console.WriteLine("Encerrando o atendimento...");
                Console.WriteLine("Obrigado pelo seu feedback! Com ele conseguimos melhorar o nosso aplicativo de animes.");
                break;
            }
        }
    }
}

// Classe Usuario
class Usuario
{
    public string Nome { get; private set; }
    public int Idade   { get; private set; }

    public Usuario(string nome, int idade)
    {
        this.Nome  = nome;
        this.Idade = idade;
    }
}

// Classe Reclamação
class Reclamacao
{
    public string Descricao { get; set; }
    public Usuario Usuario  { get; set; }

    public Reclamacao(string descricao, Usuario usuario)
    {
        this.Descricao = descricao;
        this.Usuario   = usuario;
    }
}

// Classe Anime
class Anime
{
    public string Nome                  { get; private set; }
    public string Genero                { get; private set; }
    public int    Ano                   { get; private set; }
    public List<Reclamacao> Reclamacoes { get; private set; }

    public Anime(string nome, string genero, int ano)
    {
        this.Nome        = nome;
        this.Genero      = genero;
        this.Ano         = ano;
        this.Reclamacoes = new List<Reclamacao>();
    }

    public string Detalhes()
    {
        return $"Nome: {Nome}, Gênero: {Genero}, Ano: {Ano}";
    }

    public void AdicionarReclamacao(Reclamacao reclamacao)
    {
        Reclamacoes.Add(reclamacao);
    }

    public void ExibirReclamacoes()
    {
        Console.WriteLine($"Reclamações sobre o anime '{Nome}':");
        if (Reclamacoes.Count == 0)
        {
            Console.WriteLine("Não há reclamações registradas para o anime que você escolheu.");
        }
        else
        {
            foreach (var reclamacao in Reclamacoes)
            {
                Console.WriteLine($" Reclamação: {reclamacao.Descricao} - feita por {reclamacao.Usuario.Nome}");
            }
        }
    }
}

// Classe Catalogo
class Catalogo
{
    public List<Anime> Animes { get; private set; }

    public Catalogo()
    {
        Animes = new List<Anime>
        {
            // Animes do genero ação
            new Anime("Naruto",                          "Ação", 2002),
            new Anime("Dragon Ball Z",                   "Ação", 1989),
            new Anime("One Punch Man",                   "Ação", 2015),
            new Anime("Attack on Titan",                 "Ação", 2013),
            new Anime("Chainsaw Man",                    "Ação", 2022),
            new Anime("Bleach",                          "Ação", 2004),
            new Anime("Fullmetal Alchemist Brotherhood", "Ação", 2009),
            new Anime("Jujutsu no Kaisen",               "Ação", 2020),
            new Anime("Hunter x Hunter",                 "Ação", 2011),
            new Anime("Vinland Saga",                    "Ação", 2019),

            // Animes do genero romance
            new Anime("Your Name",                    "Romance", 2016),
            new Anime("Toradora",                     "Romance", 2008),
            new Anime("Clannad",                      "Romance", 2007),
            new Anime("Fruits Basket",                "Romance", 2001),
            new Anime("Kimi no Na wa",                "Romance", 2016),
            new Anime("Nana",                         "Romance", 2006),
            new Anime("Lovely Complex",               "Romance", 2007),
            new Anime("Anohana",                      "Romance", 2011),
            new Anime("Erased",                       "Romance", 2016),
            new Anime("Steins Gate",                  "Romance", 2011),

            // Animes do genero comédia
            new Anime("Gintama",                      "Comédia", 2005),
            new Anime("One Piece",                    "Comédia", 1999),
            new Anime("Konosuba",                     "Comédia", 2016),
            new Anime("Mob Psycho 100",               "Comédia", 2016),
            new Anime("Full Metal Panic!",            "Comédia", 2002),
            new Anime("Bocchi the Rock",              "Comédia", 2022),
            new Anime("Grand Blue",                   "Comédia", 2018),
            new Anime("K-On!",                        "Comédia", 2009),
            new Anime("Excel Saga",                   "Comédia", 1999),
            new Anime("Nichijou",                     "Comédia", 2011)
        };
    }

    // Função para exibir todas as reclamações
    public void ExibirRelatorioDeReclamacoes()
    {
        Console.WriteLine("\n--- RELATÓRIO DE RECLAMAÇÕES ---");
        bool temReclamacoes = false;
        foreach (var anime in Animes)
        {
            foreach (var reclamacao in anime.Reclamacoes)
            {
                Console.WriteLine($"Anime: {anime.Nome} - Reclamação: {reclamacao.Descricao} - Usuário: {reclamacao.Usuario.Nome} (Idade: {reclamacao.Usuario.Idade})");
                temReclamacoes = true;
            }
        }

        if (!temReclamacoes)
        {
            Console.WriteLine("Não há reclamações registradas para nenhum anime.");
        }
    }

    // Exibe reclamações de um anime em especifico
    public void ExibirReclamacoesDeAnimeEspecifico()
    {
        Console.WriteLine("Digite o nome do anime para ver suas reclamações:");
        string nomeAnime = Console.ReadLine().ToLower();

        Anime anime = Animes.Find(a => a.Nome.ToLower() == nomeAnime);

        if (anime  != null)
        {
            anime.ExibirReclamacoes();
        }
        else
        {
            Console.WriteLine("Anime não encontrado.");
        }
    }

    // Função para pesquisar o anime pelo nome
    public void PesquisarPorAnime(Usuario usuario)
    {
        Console.WriteLine("Digite o nome do anime:");
        string nome = Console.ReadLine().ToLower();

        Anime anime = Animes.Find(a => a.Nome.ToLower() == nome);

        if (anime  != null)
        {
            Console.WriteLine($"Escolhendo anime '{anime.Nome}' para registrar a reclamação.");
            MenuReclamacaoAnime(usuario, anime);
        }
        else
        {
            Console.WriteLine("Anime não encontrado.");
        }
    }

    // Função para pesquisar anime pelo genero
    public void PesquisarPorGenero()
    {
        Console.WriteLine("Digite o gênero do anime que você está procurando:");
        string genero = Console.ReadLine().ToLower();

        var animesEncontrados = Animes.FindAll(a => a.Genero.ToLower() == genero);

        if (animesEncontrados.Count == 0)
        {
            Console.WriteLine("Nenhum anime encontrado para esse gênero.");
        }
        else
        {
            Console.WriteLine("\nAnimes encontrados com o gênero '" + genero + "':");
            foreach (var anime in animesEncontrados)
            {
                Console.WriteLine(anime.Detalhes());
            }
        }
    }

    // Menu de reclamação de animes
    public void MenuReclamacaoAnime(Usuario usuario, Anime anime = null)
    {
        if (anime == null)
        {
            Console.WriteLine("Escolha o anime para registrar uma reclamação:");
            for (int i = 0; i < Animes.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {Animes[i].Nome}");
            }
            int escolha        = 0;
            bool escolhaValida = false;

            while (!escolhaValida)
            {
                escolhaValida = int.TryParse(Console.ReadLine(), out escolha);
                if (escolhaValida && escolha >= 1 && escolha <= Animes.Count)
                {
                    anime         = Animes[escolha - 1];
                    escolhaValida = true;
                }
                else
                {
                    Console.WriteLine("Escolha inválida. Por favor, escolha um número válido.");
                }
            }
        }

        Console.WriteLine("Digite a descrição da sua reclamação sobre o anime:");
        string descricao      = Console.ReadLine().ToLower();
        Reclamacao reclamacao = new Reclamacao(descricao, usuario);
        anime.AdicionarReclamacao(reclamacao);

        Console.WriteLine("Sua reclamação foi registrada e será analisada por nossa equipe. Muito obrigado!");
    }

    // Função ppara gravar as reclamações
    public void GravarReclamacoes(string caminho)
    {
        using (StreamWriter file = new StreamWriter(caminho))
        { 
            foreach (var anime in Animes)
            {
                foreach (var reclamacao in anime.Reclamacoes)
                {
                    file.WriteLine($"Anime: {anime.Nome} - Reclamação: {reclamacao.Descricao} - Usuário: {reclamacao.Usuario.Nome} - Idade: {reclamacao.Usuario.Idade}");
                }
            }
        }

        Console.WriteLine("Reclamações salvas no arquivo.");
    }

    // Função para mostrar as reclamações
    public void CarregarReclamacoes(string caminho)
    {
        if (File.Exists(caminho))
        {
            string[] linhas = File.ReadAllLines(caminho);
            foreach (var linha in linhas)
            {
                if (linha.Contains("Anime"))
                {
                    string[] partes            = linha.Split(new string[] { " - " }, StringSplitOptions.None);
                    string nomeAnime           = partes[0].Replace("Anime: ", "");
                    string descricaoReclamacao = partes[1].Replace("Reclamação: ", "");
                    string nomeUsuario         = partes[2].Replace("Usuário: ", "");
                    int idadeUsuario           = int.Parse(partes[3].Replace("Idade: ", ""));

                    Anime anime     = Animes.Find(a => a.Nome == nomeAnime);
                    Usuario usuario = new Usuario(nomeUsuario, idadeUsuario);
                    if (anime      != null)
                    {
                        anime.AdicionarReclamacao(new Reclamacao(descricaoReclamacao, usuario));
                    }
                }
            }
        }
    }
}
