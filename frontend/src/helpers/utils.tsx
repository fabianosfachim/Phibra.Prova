import { format, parseISO } from "date-fns";
import ptBR from "date-fns/locale/pt-BR";

export const formateDate = (date: string) => {
  return format(parseISO(date), "dd/MM/yyyy", {
    locale: ptBR,
  });
};

export const formatCurrency = (currency: number) => {
  return Intl.NumberFormat("pt-BR").format(currency);
};

export const currencyMask = (currency: string) => {
  let value: any = currency.replaceAll(".", "").replaceAll(",", "");
  const valLength: number = currency.length;

  if (valLength >= 2) {
    value = Number(
      `${value.substring(0, valLength - 3)}.${value.substring(valLength - 3)}`
    );
  }

  return formatCurrency(value);
};

export const removeCurrencyMask = (value: string) => {
    return value.replaceAll('.', '').replaceAll(',', '.');
}
