import { faker } from "@faker-js/faker";
import { test, expect } from "@playwright/test";

test("Adicionar Transação", async ({ page }) => {
  const descricao = faker.lorem.words(3);
  const valor = faker.number.int({ min: 10, max: 1000 });
  const pessoa = faker.person.firstName();

  await page.goto("http://localhost:5173/");
  await page
    .locator(
      "#root > div.min-h-screen.app-root > div > aside > nav > ul > li:nth-child(2) > a",
    )
    .click();

  await page.waitForTimeout(5000);

  await page.getByRole("button", { name: "Adicionar Transação" }).click();

  await page.locator("#descricao").fill(descricao);
  await page.locator("#valor").fill(valor.toString());
  await page.locator("#data").fill("2025-02-17");

  // SELECT normal
  await page.locator("#tipo").selectOption({ label: "Despesa" });

  // 🔥 Campo autocomplete (tipo select fake)
  await page.locator("#pessoa-select").click();
  await page.locator("#pessoa-select").fill("Dr. Emanuel Souza");
  await page.locator("#pessoa-select-options > div").click();

  // Mesmo padrão para categoria
  await page.locator("#categoria-select").click();
  await page.locator("#categoria-select").fill("Alimentação");
  await page.locator("#categoria-select-options > div");

  await page.getByRole("button", { name: "Salvar" }).click();
});
