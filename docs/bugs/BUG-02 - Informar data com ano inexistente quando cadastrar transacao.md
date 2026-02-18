# BUG-02 - Informar data com ano inexistente quando cadastrar transacao

## 📌 Regra de Negócio Afetada

Datas de transações informada com valores inexistentes.

---

## 🎯 Severidade

Alta

---

## 🧪 Ambiente

- Backend: .NET
- Frontend: React

---

## 📖 Descrição

Podemos cadastrar despesa com data incorreta.

---

## ✅ Comportamento Esperado

O sistema deve bloquear a operação e exibir mensagem de erro informando que menores de idade não podem cadastrar receitas.

---

## ❌ Comportamento Atual

O sistema permite o cadastrar despesa com data errada.

---

## 📎 Evidência

Teste automatizado relacionado:

`frontend-tests/e2e/menor-idade.spec.ts`

## ![alt text](image-1.png)

![alt text](image-2.png)

## 💡 Possível Causa

Ausência de validação no backend ou validação apenas na camada de interface.
