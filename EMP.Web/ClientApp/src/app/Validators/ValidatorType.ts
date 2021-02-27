import { FormControl } from '@angular/forms';

type Validator<T extends FormControl> = (c: T) => { [error: string]: any };
