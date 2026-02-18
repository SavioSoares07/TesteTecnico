# 🧪 Teste Técnico - Desenvolvedor de Testes

Repositório dedicado à implementação da pirâmide de testes para o sistema de controle de gastos residenciais. O foco principal foi a validação das regras de negócio sem alterar o código original da aplicação.

---

## 🎯 Objetivo do Projeto

- **Análise de Domínio:** Extração das regras de negócio a partir do código existente.
- **Integridade:** Validar o comportamento do sistema sem modificar sua implementação.
- **Arquitetura de Testes:** Projetar e implementar uma pirâmide de testes eficiente.
- **Qualidade:** Identificar falhas críticas através de automação.

---

## 🏗️ Estratégia de Testes

A estratégia adotada segue a **Pirâmide de Testes**, garantindo uma base sólida de testes rápidos e um topo focado na experiência do usuário.

| Nível          | Tecnologia     | Foco                                                |
| :------------- | :------------- | :-------------------------------------------------- |
| **Unitários**  | .NET (xUnit)   | Regras de negócio e lógica de domínio isolada.      |
| **Integração** | .NET           | Persistência, API e comunicação com banco de dados. |
| **Componente** | React (Vitest) | Comportamento da UI e estados dos componentes.      |
| **End-to-End** | Playwright     | Fluxos completos de ponta a ponta (User Journey).   |

---

## 🧱 Regras de Negócio Validadas

As seguintes regras foram cobertas pelas suítes de testes:

- [x] **Restrição de Idade:** Menores de idade não podem cadastrar receitas.
- [x] **Consistência de Categoria:** Uso conforme finalidade (Receita / Despesa / Ambas).
- [x] **Integridade Referencial:** Exclusão em cascata de transações ao excluir uma pessoa.
- [x] **Cálculos de Totais:** Validação da soma de saldos e consultas por pessoa.

---

## 🚀 Como Executar os Testes

### 🔺 Backend (Unitários e Integração)

```bash
# Navegue até a pasta do backend e execute:
dotnet test
```

# Navegue até a pasta do frontend e execute:

npm install
npm run test

# Instalar dependências e navegadores:

npm install
npx playwright install

# Executar testes:

npx playwright test

# Abrir interface visual do Playwright:

npx playwright test --headed

├── backend-tests/
│ ├── unit/ # Testes de unidade (.NET)
│ └── integration/ # Testes de integração (.NET)
├── frontend-tests/
│ ├── component/ # Testes de componente (Vitest)
│ ├── e2e/ # Testes de fluxo (Playwright)
│ └── utils/ # Helpers e massas de dados
