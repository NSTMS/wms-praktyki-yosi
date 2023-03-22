import { productLocation } from '@static/types/locationTypes';

export type magazine = {
  id: number;
  name: string;
  address: string;
  locations?: productLocation[];
};

export type magazineToAdd = {
  name: string;
  address: string;
};
