import { Layout, Menu, theme } from "antd";

const { Header, Content, Footer } = Layout;

import "./App.css";

import { createBrowserRouter, RouterProvider } from "react-router-dom";

import Movimentation from './routes/Movimatation/movimentation.tsx';
import CriarMov from "./routes/Movimatation/create-movimentation.tsx";

const router = createBrowserRouter([
  { path: '/', element: <Movimentation /> },
  { path: '/movimentacao', element: <Movimentation /> },
  { path: '/movimentacao/criar', element: <CriarMov /> },
  { path: '/movimentacao/editar/:id', element: <CriarMov /> },
])

function App() {
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const menu = [
    {
      key: 1,
      label: `Movimentação`,
      
    },
  ];

  return (
    <>
      <Layout className="layout">
        <Header style={{ display: "flex", alignItems: "center" }}>
          <div className="demo-logo" />
          <Menu
            theme="dark"
            mode="horizontal"
            items={menu}
          />
        </Header>
        <Content style={{ padding: "0 50px" }}>
          <div
            className="site-layout-content"
            style={{ background: colorBgContainer }}
          >
            <RouterProvider router={router} />
          </div>
        </Content>
        <Footer style={{ textAlign: "center" }}>
          Ant Design ©2023 Created by Ant UED
        </Footer>
      </Layout>
    </>
  );
}

export default App;
