import { faker } from "@faker-js/faker";
import { test, expect } from "@playwright/test";

test("Adicionar Categoria", async ({ page }) => {
  const descricao = faker.lorem.words(3);
  await page.goto("http://localhost:5173/");
  await page
    .locator(
      "#root > div.min-h-screen.app-root > div > aside > nav > ul > li:nth-child(3) > a",
    )
    .click();

  await page.getByRole("button", { name: "Adicionar Categoria" }).click();

  await page.locator("#descricao").fill(descricao);

  await page.getByRole("button", { name: "Salvar" });
});
