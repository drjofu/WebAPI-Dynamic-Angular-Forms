import { PropertyDescriptorBase } from "./property-descriptor-base";


export class PropertyDescriptorSelector extends PropertyDescriptorBase {
  controlType = 'dropdown';

  // Daten für DropDown-Feld
  options: { key: string, value: string }[] = [];

  // Url für Daten
  lookupUrl: string;

  // Name des ID-Felds
  lookupIdField: string;

  // Name des Felds für die Wertanzeige
  lookupValueField: string;

}
