# WinMaintenanceTool

Aplicativo desktop em **C# 14 + .NET 10** (WPF + WPF-UI 4.1) para manutenção de integridade do Windows com foco em duas frentes principais:

- **SFC** para validação/reparo de arquivos protegidos do sistema.
- **DISM** para diagnóstico e reparo da imagem/componente store do Windows.

> Objetivo central: reduzir fricção para executar manutenção essencial sem depender de comandos manuais no Prompt de Comando.

---

## Proposta de UX do aplicativo

A experiência foi desenhada para ser **direta e orientada por tarefa**:

1. O usuário abre o app e cai no **Painel inicial**.
2. Escolhe o fluxo pelo **card grande** (SFC ou DISM).
3. Dentro do fluxo, toca diretamente no **card da ação** desejada.
4. Acompanha tudo pelo **log de execução** com progresso.

### Decisões de navegação

- A navegação superior foi simplificada para ações globais: **Início** e **Configurações**.
- A escolha entre **SFC/DISM** acontece via cards na Home (evita redundância visual).
- As ações de execução também são acionadas por cards, melhorando descobribilidade.

---

## Estrutura do projeto

- `src/WinMaintenanceTool/Assets` → ícones e imagens.
- `src/WinMaintenanceTool/Models` → modelos de domínio.
- `src/WinMaintenanceTool/Services` → serviços de execução de comandos, configuração e localização.
- `src/WinMaintenanceTool/ViewModels` → lógica MVVM.
- `src/WinMaintenanceTool/Views` → interface XAML.
- `src/WinMaintenanceTool/Resources` → strings localizadas (`.resx`) para inglês e português.
- `build.ps1` → build portátil `self-contained/single-file` para `Build/`.

---

## Fluxos de manutenção

## SFC

- **Verificação completa de integridade** (`sfc /scannow`)
- **Somente verificação** (`sfc /verifyonly`)

Use SFC quando houver suspeita de corrupção de arquivos do sistema (instabilidade, falhas de apps nativos, etc.).

## DISM

- **CheckHealth**
- **ScanHealth**
- **RestoreHealth**
- **AnalyzeComponentStore**
- **StartComponentCleanup**
- **StartComponentCleanup /ResetBase**

Use DISM para corrigir base da imagem do Windows, especialmente após falhas recorrentes em atualizações ou corrupção no componente store.

---

## Resiliência e confiabilidade

- Tratamento de exceções globais no ciclo de vida do app.
- Escrita de log de erro de inicialização em `%LOCALAPPDATA%/WinMaintenanceTool/startup.log`.
- Execução de comandos com captura contínua de saída e erro.
- Atualização de UI desacoplada para evitar falhas por callbacks assíncronos.

---

## Build

Execute no PowerShell (Windows):

```powershell
./build.ps1
```

Saída final: pasta `Build/`.
