
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";

import React from "react";

import RootLayout from "./navigation/RootLayout";
import App from "./App";

const rootElement = document.getElementById("root");
const root = createRoot(rootElement);

root.render(
  <StrictMode>
    <App />
  </StrictMode>
);
