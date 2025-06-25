import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
// FIXME: подключение пустого файла
import "./index.css";
import App from "./App.tsx";
import { Provider } from "./components/ui/provider.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <Provider>
      <App />
    </Provider>
  </StrictMode>
);
