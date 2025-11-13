using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class GameApiSersce
{
    private readonly HttpClient httpClient;
    private const string BASE_URL = "https://68f97508ef8b2e621e7c1fb8.mockapi.io/";
    public string idteste;

    public GameApiSersce()
    {
        httpClient = new HttpClient();
    }


    void Start()
    {
        idteste = "1";
    }



    public async Task<script[]> GetTodosJogadores()
    {
        try
        {
            string url =
            $"{BASE_URL}/players";
            Debug.Log($"GET: {url}");
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Resposta recebida:{json.Substring(0, Math.Min(200, json.Length))} ...");


            string wrappedJson = $"{{\"jogadores\":{json}}}";
            JogadorArray jogadorArray = JsonUtility.FromJson<JogadorArray>(wrappedJson);
            return jogadorArray.jogadores;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar jogadores: {ex.Message} ");
            return new script[0];
        }
    }

    public async Task<script> GetJogador(string id)
    {
        try
        {
            string url = $"{BASE_URL}/players/{id}";
            Debug.Log($"GET: {url}");

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador recebido: {json}");

            script jogador = JsonUtility.FromJson<script>(json);
            return jogador;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar jogador {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<script> AtualizarJogador(script player)
    {
        try
        {
            string url = $"{BASE_URL}/players/1";
            Debug.Log($"PUT: {url}");

            string json = JsonUtility.ToJson(player);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync(url, content);

            Debug.Log($"Status da resposta: {response.StatusCode}");
            Debug.Log($"Resposta: {await response.Content.ReadAsStringAsync()}");

            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador atualizado: {responseJson}");

            return JsonUtility.FromJson<script>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar jogador: {ex.Message}");
            return null;
        }
    }

    public async Task<script> CriarJogador(script player)
    {
        try
        {
            string url = $"{BASE_URL}/players";
            Debug.Log($"POST: {url}");

            string json = JsonUtility.ToJson(player);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador criado: {responseJson}");

            return JsonUtility.FromJson<script>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao criar jogador: {ex.Message}");
            return null;
        }
    }



    [System.Serializable]
    public class JogadorArray
    {
        public script[] jogadores;
    }




}
