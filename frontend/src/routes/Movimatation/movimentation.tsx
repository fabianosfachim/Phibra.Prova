import { Table, Button, Space } from "antd";
import type { ColumnsType } from "antd/es/table";
import { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { formatCurrency, formateDate } from "../../helpers/utils";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";

const apiUrl = "https://localhost:7053/api/Movimentacao";

function Movimentation() {
  const columns: ColumnsType<any> = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Data Movimento",
      dataIndex: "dt_lancamento",
      key: "dt_lancamento",
    },
    {
      title: "Data Lançamento",
      dataIndex: "dt_movimento",
      key: "dt_movimento",
    },
    {
      title: "Valor",
      dataIndex: "vl_lancamento",
      key: "vl_lancamento",
    },
    {
      title: "Ações",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <a onClick={() => editMov(record.id)}>
            <EditOutlined />
          </a>
          <a onClick={() => deleteMov(record.id)}>
            <DeleteOutlined />
          </a>
        </Space>
      ),
    },
  ];

  const navigate = useNavigate();
  const MySwal = withReactContent(Swal);
  const [movimentationList, setMovimentationList] = useState<any>(null);

  function goToNew() {
    navigate("/movimentacao/criar");
  }

  function editMov(id: number) {
    navigate(`/movimentacao/editar/${id}`);
  }

  function deleteMov(id: number) {
    MySwal.fire({
      title: "Atenção",
      text: "Você tem certeza de que deseja excluir essa movimentação?",
      showCancelButton: true,
      confirmButtonText: "Confirmar",
      icon: "warning",
    }).then((result: any) => {
      if (result.isConfirmed) {
        axios
          .post(`${apiUrl}/ExcluirMovimentacao?id=${id}`, {})
          .then((res: any) => {
            MySwal.fire({
              title: "Sucesso",
              text: "Movimentação excluida com sucesso",
              icon: "success",
            }).then(() => {
              getAll();
            });
          })
          .catch((err: any) => {
            MySwal.fire({
              title: "Ops",
              text: "Falha ao tentar excluir a movimentação",
              icon: "error",
            });
          });
      }
    });
  }

  function getAll() {
    axios.get(`${apiUrl}/ListarMovimentacao`).then((res: any) => {
      const data = res.data.data;

      data.movimentacao = data.movimentacao.map((mov: any) => {
        mov.dt_lancamento = formateDate(mov.dt_lancamento);
        mov.dt_movimento = formateDate(mov.dt_movimento);
        mov.vl_lancamento = `R$ ${formatCurrency(mov.vl_lancamento)}`;

        return mov;
      });

      setMovimentationList(data.movimentacao);
    });
  }

  useEffect(() => {
    getAll();
  }, []);

  return (
    <div>
      <div style={{ marginBottom: 16 }}>
        <Button onClick={goToNew} type="primary">
          Novo
        </Button>
      </div>
      <Table columns={columns} dataSource={movimentationList} />
    </div>
  );
}

export default Movimentation;
