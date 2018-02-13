export class PropertyDescriptorBase {

  // Feldname
  name: string;

  // Anzeigetext
  label: string;

  // Sortierung
  order: number;

  // Art des darzustellenden Controls
  controlType: string;

  // type-Attribut für <input>
  displayType: string;

  // Validierungsattribute
  required: boolean;
  minLength: number;
  maxLength: number;
  minimum: number;
  maximum: number;

  // Sichtbarkeit im Editor und in der Tabelle
  visible: boolean;

  // Schreibschutz im Editor
  readOnly: boolean;

  // eigenes Attribut
  evenNumber: boolean;
  dividableBy: number | null;
}
