import { test, expect } from "@playwright/test";
import { fakerPT_BR as faker } from "@faker-js/faker";

//Adicionar pessoa

test("Adicionar pessoa", async ({ page }) => {
  await page.goto("http://localhost:5173/");
  await page.locator("a[href='/pessoas']").click();

  await page.getByRole("button", { name: "Adicionar Pessoa" }).click();

  const nome = faker.person.fullName();
  const dataNascimento = faker.date.birthdate({
    min: 5,
    max: 60,
    mode: "age",
  });
  await page.locator("input[id=nome]").fill(nome);
  await page
    .locator("input[id=dataNascimento]")
    .fill(dataNascimento.toISOString().split("T")[0]);

  await page.getByRole("button", { name: "Salvar" }).click();
});

//Editar pessoa

test("Editar pessoa", async ({ page }) => {
  await page.goto("http://localhost:5173/");
  await page.locator("a[href='/pessoas']").click();

  await page.getByRole("button", { name: "Editar" }).first().click();

  const nome = faker.person.fullName();
  const dataNascimento = faker.date.birthdate({
    min: 5,
    max: 60,
    mode: "age",
  });
  await page.locator("input[id=nome]").fill(nome);
  await page
    .locator("input[id=dataNascimento]")
    .fill(dataNascimento.toISOString().split("T")[0]);

  await page.getByRole("button", { name: "Salvar" }).click();
});

//Excluir pessoa

test("Excluir pessoa", async ({ page }) => {
  await page.goto("http://localhost:5173/");
  await page.locator("a[href='/pessoas']").click();

  await page.getByRole("button", { name: "Deletar" }).first().click();

  await page.getByRole("button", { name: "Confirmar" });
});
