import { DatePicker, Input, Select, Space, DatePickerProps } from "antd";
import { useForm } from "react-hook-form";
import axios from "axios";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import {
  currencyMask,
  formatCurrency,
  removeCurrencyMask,
} from "../../helpers/utils";
import moment from "moment";

function CriarMov() {
  const { register, handleSubmit } = useForm();
  const navigation = useNavigate();
  const MySwal = withReactContent(Swal);
  const params = useParams();

  const [controlledDate, setControlledDate] = useState<any>(null);
  const [controlledDateMov, setControlledDateMov] = useState<any>(null);
  const [movType, setMovType] = useState<any>(null);
  const [movValue, setMovValue] = useState<any>(null);
  const [isEdit, setIsEdit] = useState<boolean>(false);

  const apiUrl = "https://localhost:7053/api/Movimentacao";

  const changeDate: DatePickerProps["onChange"] = (date, dateString) => {
    setControlledDate(date);
  };

  const changeDateMov: DatePickerProps["onChange"] = (date, dateString) => {
    setControlledDateMov(date);
  };

  const changeMovValue = (event: any) => {
    setMovValue(currencyMask(event.currentTarget.value));
  };

  function goBack() {
    navigation("/movimentacao");
  }

  function createMov(formInput: any) {
    const input: any = {
      movimentacao: {
        id: isEdit ? params.id : 0,
        id_movimentacao: movType,
        dt_movimento: new Date(controlledDate),
        dt_lancamento: new Date(controlledDateMov),
        vl_lancamento: removeCurrencyMask(movValue),
      },
    };

    if (isEdit) {
      axios
        .post(`${apiUrl}/AtualizarMovimentacao`, input)
        .then((res: any) => {
          MySwal.fire({
            title: "Sucesso",
            text: "Movimentação atualizada com sucesso",
            icon: "success",
          }).then(() => {
            navigation("/movimentacao");
          });
        })
        .catch((err: any) => {
          MySwal.fire({
            title: "Ops",
            text: "Falha ao tentar atualizar a movimentação",
            icon: "error",
          });
        });
    } else {
      axios
        .post(`${apiUrl}/AdicionarMovimentacao`, input)
        .then((res: any) => {
          MySwal.fire({
            title: "Sucesso",
            text: "Movimentação adicionada com sucesso",
            icon: "success",
          }).then(() => {
            navigation("/movimentacao");
          });
        })
        .catch((err: any) => {
          MySwal.fire({
            title: "Ops",
            text: "Falha ao tentar adicionar a movimentação",
            icon: "error",
          });
        });
    }
  }

  function getMovById(id: any) {
    axios.get(`${apiUrl}/ListarMovimentacaoId?id=${id}`).then((res: any) => {
      const data: any = res.data.data;

      setMovType(data.objmovimentacao.id_movimentacao);
      setControlledDate(
        moment(data.objmovimentacao.dt_movimento, "YYYY/MM/DD")
      );
      setControlledDateMov(
        moment(data.objmovimentacao.dt_lancamento, "YYYY/MM/DD")
      );
      setMovValue(formatCurrency(data.objmovimentacao.vl_lancamento));
    });
  }

  useEffect(() => {
    setIsEdit(false);

    if (params.id) {
      setIsEdit(true);
      getMovById(params.id);
    }
  }, []);

  return (
    <form onSubmit={handleSubmit(createMov)}>
      <div className="row">
        <div className="col-md-6 d-flex flex-column">
          <label htmlFor="">Tipo</label>
          <Select
            options={[
              { value: 1, label: "Receitas" },
              { value: 2, label: "Despesas" },
            ]}
            value={movType}
            onChange={setMovType}
          />
        </div>
        <div className="col-md-6 d-flex flex-column">
          <label htmlFor="">Data</label>
          <DatePicker value={controlledDate} onChange={changeDate} />
        </div>
        <div className="col-md-6  d-flex flex-column">
          <label htmlFor="">Data do Lançamento</label>
          <DatePicker value={controlledDateMov} onChange={changeDateMov} />
        </div>
        <div className="col-md-6 d-flex flex-column">
          <label htmlFor="">Valor</label>
          <Input type="text" value={movValue} onChange={changeMovValue} />
        </div>
      </div>
      <div className="row mt-4">
        <div className="col-md-12">
          <Space>
            <button className="btn btn-danger" onClick={goBack}>
              Cancelar
            </button>
            {isEdit ? (
              <button className="btn btn-primary" type="submit">
                Editar
              </button>
            ) : (
              <button className="btn btn-primary" type="submit">
                Criar
              </button>
            )}
          </Space>
        </div>
      </div>
    </form>
  );
}

export default CriarMov;
