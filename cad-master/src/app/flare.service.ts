import { Injectable } from '@angular/core';

import { FLARE } from './mock-flare';

@Injectable()
export class FlareService {
    getFlare(): Object {
        return FLARE;
    }
}