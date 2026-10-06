const form = document.querySelector("#contact-form");
const success = document.querySelector("#form-success");

const fields = {
  naam: {
    input: document.querySelector("#naam"),
    error: document.querySelector("#naam-error"),
    validate: (value) => value.trim().length >= 2,
    message: "Vul minimaal 2 tekens in.",
  },
  email: {
    input: document.querySelector("#email"),
    error: document.querySelector("#email-error"),
    validate: (value) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value.trim()),
    message: "Vul een geldig e-mailadres in.",
  },
  bericht: {
    input: document.querySelector("#bericht"),
    error: document.querySelector("#bericht-error"),
    validate: (value) => value.trim().length >= 20,
    message: "Schrijf minimaal 20 tekens.",
  },
};

function setFieldState(field, isValid) {
  field.input.setAttribute("aria-invalid", String(!isValid));
  field.error.textContent = isValid ? "" : field.message;
}

function validateField(name) {
  const field = fields[name];
  const isValid = field.validate(field.input.value);
  setFieldState(field, isValid);
  return isValid;
}

Object.values(fields).forEach((field) => {
  field.input.addEventListener("input", () => {
    validateField(field.input.id);
    success.textContent = "";
  });
});

form.addEventListener("submit", (event) => {
  event.preventDefault();

  const valid = Object.keys(fields).map(validateField).every(Boolean);

  if (!valid) {
    success.textContent = "Controleer de invoer en verbeter de fouten.";
    success.classList.remove("show");
    return;
  }

  success.textContent = "Je bericht is succesvol verzonden!";
  success.classList.add("show");
  form.reset();

  Object.values(fields).forEach((field) => {
    field.input.removeAttribute("aria-invalid");
    field.error.textContent = "";
  });
});