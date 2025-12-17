#import <UIKit/UIKit.h>

extern "C"
{
    void ShowToast(const char* message)
    {
        NSString* msg = [NSString stringWithUTF8String:message];

        dispatch_async(dispatch_get_main_queue(), ^{
            UIWindow* window = UIApplication.sharedApplication.keyWindow;

            UILabel* toastLabel = [[UILabel alloc] initWithFrame:CGRectMake(
                window.frame.size.width / 2 - 150,
                window.frame.size.height - 100,
                300,
                40)];

            toastLabel.backgroundColor = [[UIColor blackColor] colorWithAlphaComponent:0.7];
            toastLabel.textColor = UIColor.whiteColor;
            toastLabel.textAlignment = NSTextAlignmentCenter;
            toastLabel.text = msg;
            toastLabel.alpha = 1.0;
            toastLabel.layer.cornerRadius = 10;
            toastLabel.clipsToBounds = YES;

            [window addSubview:toastLabel];

            [UIView animateWithDuration:0.5 delay:2.0 options:UIViewAnimationOptionCurveEaseOut animations:^{
                toastLabel.alpha = 0.0;
            } completion:^(BOOL finished) {
                [toastLabel removeFromSuperview];
            }];
        });
    }
}
