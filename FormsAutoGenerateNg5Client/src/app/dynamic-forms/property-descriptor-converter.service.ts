import { Injectable } from '@angular/core';
import { FormControl, Validators, FormGroup } from "@angular/forms";
import { PropertyDescriptorBase } from './property-descriptor-base';

@Injectable()
export class PropertyDescriptorConverterService {

  constructor() { }

  // Transformation der Tabellen-Metadaten in FormGroup für Reactive-Form
  toFormGroup(propertyDescriptors: PropertyDescriptorBase[], data: object) {

    // zu generierende Gruppe für Form
    let group = {};

    propertyDescriptors.forEach(propertyDescriptor => {
      // Wert ermitteln
      let value = data[propertyDescriptor.name];

      // Bei Datumsformat Date aus ISO-Format lesen
      if (propertyDescriptor.displayType == "date")
        if (value) value = new Date(value.substring(0, 10));

      // Validatoren erstellen
      let validators = [];
      if (propertyDescriptor.required) validators.push(Validators.required);
      if (propertyDescriptor.minLength) validators.push(Validators.minLength(propertyDescriptor.minLength));
      if (propertyDescriptor.maxLength) validators.push(Validators.maxLength(propertyDescriptor.maxLength));

      if (propertyDescriptor.minimum) validators.push(Validators.min(propertyDescriptor.minimum));
      if (propertyDescriptor.maximum) validators.push(Validators.max(propertyDescriptor.maximum));

      // FormControl-Instanz anlegen und der Gruppe hinzufügen
      var fc = new FormControl(value, validators);

      group[propertyDescriptor.name] = fc;

    });

    return new FormGroup(group);
  }
}
