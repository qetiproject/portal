import { Subject, Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators';

export class EventBus {
  private message$ = new Subject<Message>();
  send(message: Message): void {
    this.message$.next(message);
  }
  subscribe<T extends Message>(type: any, callback: (message: T) => void): Subscription {
    return this.message$
      .pipe(
        filter((message: Message) => message instanceof type),
        map((message: Message) => message as T)
      )
      .subscribe(callback);
  }
}

// Messages
export class Message {}
export class OkButtonClickEvent extends Message {}
export class CancelButtonClickEvent extends Message {}
export class CreateEmployeeButtonClickEvent extends Message {}
export class UpdateUserPhotoButtonClickEvent extends Message {}

// Shared
let eventBus = new EventBus();
